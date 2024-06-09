using System.Collections.Generic;
using System.Linq;
using DependencyInjection;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AnimationController))]
public class BaseGoapAgent : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyDamageDealer _enemyDamageDealer;

    [Header("Known Locations")] 
    [SerializeField] private Transform _restingPosition;
    [SerializeField] private Transform _foodPosition;

    protected NavMeshAgent navMeshAgent;
    protected AnimationController animationController;
    private Rigidbody _rb;


    [Header("Stats")] 
    [SerializeField] private float _stamina = 100f;
    [field: SerializeField]public float MaxHealth { get; set; }
    
    public float CurrentHealth { get; set; }

    private CountdownTimer _statsTimer;
    private Vector3 _destination;

    private AgentGoal _lastGoal;

    public AgentGoal currentGoal;
    public ActionPlan actionPlan;
    public AgentAction currentAction;

    public Dictionary<string, AgentBelief> beliefs;
    protected BeliefFactory factory;
    public HashSet<AgentAction> actions;
    protected HashSet<AgentGoal> goals;

    public bool _isDefending;
    public bool _isAttacked;

    [SerializeField] private GoapFactory _gFactory;
    private IGoapPlanner _gPlanner;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<AnimationController>();
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        _gPlanner = _gFactory.CreatePlanner();

        CurrentHealth = MaxHealth;
    }

    void Start()
    {
        SetupTimers();
        SetupBeliefs();
        SetupActions();
        SetupGoals();
    }

    protected virtual void SetupBeliefs()
    {
        beliefs = new Dictionary<string, AgentBelief>();
        factory = new BeliefFactory(this, beliefs);

        factory.AddBelief("Nothing", () => false);

        factory.AddBelief("AgentIdle", () => !navMeshAgent.hasPath);
        factory.AddBelief("AgentMoving", () => navMeshAgent.hasPath);
        factory.AddBelief("AgentIsHealthy", () => CurrentHealth >= 50);
        factory.AddBelief("AgentIsRested", () => _stamina >= 50);
        factory.AddBelief("AgentDefending", () => _isDefending);
        factory.AddBelief("AgentAttacked", () => _isAttacked);

        factory.AddLocationBelief("AgentAtRestingPosition", 3f, _restingPosition);
        factory.AddLocationBelief("AgentAtFoodShack", 3f, _foodPosition);
        
        factory.AddBelief("AttackingPlayer", () => false);
    }

    protected virtual void SetupActions()
    {
        actions = new HashSet<AgentAction>();

        actions.Add(new AgentAction.Builder("Relax")
            .WithStrategy(new IdleStrategy(5))
            .AddEffect(beliefs["Nothing"])
            .Build());

        actions.Add(new AgentAction.Builder("Wander Around")
            .WithStrategy(new WanderStrategy(navMeshAgent, 10))
            .AddEffect(beliefs["AgentMoving"])
            .Build());

        actions.Add(new AgentAction.Builder("MoveToEatingPosition")
            .WithStrategy(new MoveStrategy(navMeshAgent, () => _foodPosition.position))
            .AddEffect(beliefs["AgentAtFoodShack"])
            .Build());

        actions.Add(new AgentAction.Builder("MoveToRestingPosition")
            .WithStrategy(new MoveStrategy(navMeshAgent, () => _restingPosition.position))
            .AddEffect(beliefs["AgentAtRestingPosition"])
            .Build());

        actions.Add(new AgentAction.Builder("Eat")
            .WithStrategy(new IdleStrategy(5))
            .AddPrecondition(beliefs["AgentAtFoodShack"])
            .AddEffect(beliefs["AgentIsHealthy"])
            .Build());

        actions.Add(new AgentAction.Builder("Rest")
            .WithStrategy(new IdleStrategy(5))
            .AddPrecondition(beliefs["AgentAtRestingPosition"])
            .AddEffect(beliefs["AgentIsRested"])
            .Build());

        actions.Add(new AgentAction.Builder("Defend")
            .WithStrategy(new DefendStrategy(navMeshAgent, () => beliefs["TargetInDangerSensor"].Location, this, animationController))
            .AddPrecondition(beliefs["AgentAttacked"])
            .AddEffect(beliefs["AgentDefending"])
            .Build());
    }

    protected virtual void SetupGoals()
    {
        goals = new HashSet<AgentGoal>();

        goals.Add(new AgentGoal.Builder("Chill Out")
            .WithPriority(1)
            .WithDesiredEffect(beliefs["Nothing"])
            .Build());

        goals.Add(new AgentGoal.Builder("Wander")
            .WithPriority(1)
            .WithDesiredEffect(beliefs["AgentMoving"])
            .Build());

        goals.Add(new AgentGoal.Builder("KeepHealthUp")
            .WithPriority(2)
            .WithDesiredEffect(beliefs["AgentIsHealthy"])
            .Build());

        goals.Add(new AgentGoal.Builder("KeepStaminaUp")
            .WithPriority(2)
            .WithDesiredEffect(beliefs["AgentIsRested"])
            .Build());

        goals.Add(new AgentGoal.Builder("DefendFromAttacker")
            .WithPriority(5)
            .WithDesiredEffect(beliefs["AgentDefending"])
            .Build());
    }

    void SetupTimers()
    {
        _statsTimer = new CountdownTimer(2f);
        _statsTimer.OnTimerStop += () =>
        {
            UpdateStats();
            _statsTimer.Start();
        };
        
        _statsTimer.Start();
    }

    
    void UpdateStats()
    {
        _stamina += InRangeOf(_restingPosition.position, 3f) ? 20 : -10;
        CurrentHealth += InRangeOf(_foodPosition.position, 3f) ? 20 : -5;
        _stamina = Mathf.Clamp(_stamina, 0, 100);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, 100);
    }

    protected bool InRangeOf(Vector3 pos, float range) => Vector3.Distance(transform.position, pos) < range;

    protected void HandleTargetChanged()
    {
        Debug.Log("Target changed, clearing current action and goal");
        // Force the planner to re-evaluate the plan
        currentAction = null;
        currentGoal = null;
    }

    void Update()
    {
        _statsTimer.Tick(Time.deltaTime);
        animationController.SetSpeed(navMeshAgent.velocity.magnitude);

        // Update the plan and current action if there is one
        if (currentAction == null)
        {
            Debug.Log("Calculating any potential new plan");
            CalculatePlan();

            if (actionPlan != null && actionPlan.Actions.Count > 0)
            {
                navMeshAgent.ResetPath();

                currentGoal = actionPlan.AgentGoal;
                Debug.Log($"Goal: {currentGoal.Name} with {actionPlan.Actions.Count} actions in plan");
                currentAction = actionPlan.Actions.Pop();
                Debug.Log($"Popped action: {currentAction.Name}");
                // Verify all precondition effects are true
                if (currentAction.Preconditions.All(b => b.Evaluate()))
                {
                    currentAction.Start();
                }
                else
                {
                    Debug.Log("Preconditions not met, clearing current action and goal");
                    currentAction = null;
                    currentGoal = null;
                }
            }
        }

        // If we have a current action, execute it
        if (actionPlan != null && currentAction != null)
        {
            currentAction.Update(Time.deltaTime);

            if (currentAction.Complete)
            {
                Debug.Log($"{currentAction.Name} complete");
                currentAction.Stop();
                currentAction = null;

                if (actionPlan.Actions.Count == 0)
                {
                    Debug.Log("Plan complete");
                    _lastGoal = currentGoal;
                    currentGoal = null;
                }
            }
        }
    }

    void CalculatePlan()
    {
        var priorityLevel = currentGoal?.Priority ?? 0;

        HashSet<AgentGoal> goalsToCheck = goals;

        // If we have a current goal, we only want to check goals with higher priority
        if (currentGoal != null)
        {
            Debug.Log("Current goal exists, checking goals with higher priority");
            goalsToCheck = new HashSet<AgentGoal>(goals.Where(g => g.Priority > priorityLevel));
        }

        var potentialPlan = _gPlanner.Plan(this, goalsToCheck, _lastGoal);
        if (potentialPlan != null)
        {
            actionPlan = potentialPlan;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        _isAttacked = true;
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void StartDealDamage()
    {
        _enemyDamageDealer.StartDealDamage();
    }

    public void EndDealDamage()
    {
        _enemyDamageDealer.EndDealDamage();
    }
}