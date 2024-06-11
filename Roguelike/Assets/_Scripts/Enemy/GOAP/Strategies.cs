using System;
using UnityEngine;
using UnityEngine.AI;

public interface IActionStrategy
{
    bool CanPerform { get; }
    bool Complete { get; }

    void Start()
    {
        // noop
    }

    void Update(float deltaTime)
    {
        // noop
    }

    void Stop()
    {
        // noop
    }
}

public class AttackStrategy : IActionStrategy
{
    public bool CanPerform => true; // Agent can always attack
    public bool Complete { get; private set; }

    private readonly CountdownTimer _timer;
    private readonly AnimationController _animations;
    private readonly BaseGoapAgent _agent;

    public AttackStrategy(AnimationController animations)
    {
        _animations = animations;
        _timer = new CountdownTimer(animations.GetAnimationLength(animations.attackClip));
        _timer.OnTimerStart += () => Complete = false;
        _timer.OnTimerStop += () => Complete = true;
    }

    public void Start()
    {
        _timer.Start();
        _animations.Attack();
    }

    public void Update(float deltaTime) => _timer.Tick(deltaTime);
}

public class DefendStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly Func<Vector3> target;

    public bool CanPerform => !Complete;
    public bool Complete { get; private set; }

    private readonly CountdownTimer _timer;
    private readonly AnimationController _animations;
    private readonly BaseGoapAgent _agent;

    public DefendStrategy(NavMeshAgent agent, Func<Vector3> target, BaseGoapAgent baseGoapAgent, AnimationController animations)
    {
        this.agent = agent;
        this.target = target;
        _agent = baseGoapAgent;
        _animations = animations;

        _timer = new CountdownTimer(animations.GetAnimationLength(animations.attackClip));
        _timer.OnTimerStart += () => Complete = false;
        _timer.OnTimerStop += () => Complete = true;
    }

    public void Start()
    {
        agent.SetDestination(target());
    }

    public void Update(float deltaTime)
    {
        _timer.Tick(deltaTime);

        
        if (target != null)
        {
            if (Vector3.Distance(agent.transform.position, target()) > 5f)
            {
                Complete = true;
                return;
            }
            
            
            Vector3 directionToTarget = (target() - agent.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            agent.transform.rotation = lookRotation;
        
            _animations.Attack();
        }
        else
        {
            Stop();
        }
    }

    public void Stop()
    {
        _agent._isDefending = false;
        _agent._isAttacked = false;
    }
}


public class RunStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    private readonly Func<Vector3> target;
    private float originalSpeed;

    public bool CanPerform => !Complete;
    public bool Complete => agent.remainingDistance <= 2f && !agent.pathPending;

    public RunStrategy(NavMeshAgent agent, Func<Vector3> target)
    {
        this.agent = agent;
        this.target = target;
        originalSpeed = agent.speed;
    }

    public void Start()
    {
        agent.speed *= 2;

        var position = agent.transform.position;
        Vector3 directionToClosestAttacker = (target() - position);

        Vector3 runToPoint = position - directionToClosestAttacker.normalized * agent.speed;

        agent.SetDestination(runToPoint);
    }


    public void Stop()
    {
        agent.ResetPath();
        agent.speed = originalSpeed;
    }
}

public class MoveStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly Func<Vector3> destination;

    public bool CanPerform => !Complete;
    public bool Complete => agent.remainingDistance <= 2f && !agent.pathPending;

    public MoveStrategy(NavMeshAgent agent, Func<Vector3> destination)
    {
        this.agent = agent;
        this.destination = destination;
    }

    public void Start() => agent.SetDestination(destination());


    public void Stop() => agent.ResetPath();
}

public class WanderStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly float wanderRadius;

    public bool CanPerform => !Complete;
    public bool Complete => agent.remainingDistance <= 2f && !agent.pathPending;

    public WanderStrategy(NavMeshAgent agent, float wanderRadius)
    {
        this.agent = agent;
        this.wanderRadius = wanderRadius;
    }

    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomDirection = (UnityEngine.Random.insideUnitSphere * wanderRadius).With(y: 0);
            NavMeshHit hit;

            if (NavMesh.SamplePosition(agent.transform.position + randomDirection, out hit, wanderRadius, 1))
            {
                agent.SetDestination(hit.position);
                return;
            }
        }
    }
}

public class IdleStrategy : IActionStrategy
{
    public bool CanPerform => true; // Agent can always Idle
    public bool Complete { get; private set; }

    readonly CountdownTimer timer;

    public IdleStrategy(float duration)
    {
        timer = new CountdownTimer(duration);
        timer.OnTimerStart += () => Complete = false;
        timer.OnTimerStop += () => Complete = true;
    }

    public void Start() => timer.Start();
    public void Update(float deltaTime) => timer.Tick(deltaTime);
}