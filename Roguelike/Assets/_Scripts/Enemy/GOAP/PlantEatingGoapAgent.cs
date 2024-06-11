using UnityEngine;

public class PlantEatingGoapAgent : BaseGoapAgent
{
    [Header("Sensors")] 
    [SerializeField] private Sensor _dangerSensor;

    protected override void SetupBeliefs()
    {
        base.SetupBeliefs();
        
        factory.AddSensorBelief("TargetInDangerSensor", _dangerSensor);

        factory.AddBelief("AwayFromDanger", () => !InRangeOf(_dangerSensor.TargetPosition, 3f));
    }
    
    protected override void SetupActions()
    {
        base.SetupActions();
        
        actions.Add(new AgentAction.Builder("RunFromDanger")
            .WithStrategy(new RunStrategy(navMeshAgent, () => beliefs["TargetInDangerSensor"].Location))
            .AddPrecondition(beliefs["TargetInDangerSensor"])
            .AddEffect(beliefs["AwayFromDanger"])
            .Build());
    }

    protected override void SetupGoals()
    {
        base.SetupGoals();
        
        goals.Add(new AgentGoal.Builder("RunFromDanger")
            .WithPriority(3)
            .WithDesiredEffect(beliefs["AwayFromDanger"])
            .Build());
    }
    
    void OnEnable() => _dangerSensor.OnTargetChanged += HandleTargetChanged;
    void OnDisable() => _dangerSensor.OnTargetChanged -= HandleTargetChanged;
}
