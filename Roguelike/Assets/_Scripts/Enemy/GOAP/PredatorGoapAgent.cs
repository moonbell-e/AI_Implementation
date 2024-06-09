using UnityEngine;

public class PredatorGoapAgent : BaseGoapAgent
{
    [Header("Sensors")] 
    [SerializeField] private Sensor _chaseSensor;
    [SerializeField] private Sensor _attackSensor;
    
    void OnEnable() => _chaseSensor.OnTargetChanged += HandleTargetChanged;
    void OnDisable() => _chaseSensor.OnTargetChanged -= HandleTargetChanged;
    
    protected override void SetupBeliefs()
    {
        base.SetupBeliefs();
        
        factory.AddSensorBelief("TargetInDangerSensor", _attackSensor);
        factory.AddSensorBelief("PlayerInChaseRange", _chaseSensor);
        factory.AddSensorBelief("PlayerInAttackRange", _attackSensor);
    }
    
    protected override void SetupActions()
    {
        base.SetupActions();
        
        actions.Add(new AgentAction.Builder("ChasePlayer")
            .WithStrategy(new MoveStrategy(navMeshAgent, () => beliefs["PlayerInChaseRange"].Location))
            .AddPrecondition(beliefs["PlayerInChaseRange"])
            .AddEffect(beliefs["PlayerInAttackRange"])
            .Build());
        
        actions.Add(new AgentAction.Builder("AttackTarget")
            .WithStrategy(new AttackStrategy(animationController))
            .AddPrecondition(beliefs["PlayerInAttackRange"])
            .AddEffect(beliefs["AttackingPlayer"])
            .Build());
    }
    
    protected override void SetupGoals()
    {
        base.SetupGoals();
        
        goals.Add(new AgentGoal.Builder("SeekAndDestroy")
            .WithPriority(4)
            .WithDesiredEffect(beliefs["AttackingPlayer"])
            .Build());
    }
}
