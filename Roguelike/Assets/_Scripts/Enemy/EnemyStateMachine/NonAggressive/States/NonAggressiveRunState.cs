using UnityEngine;

public class NonAggressiveRunState : NonAggressiveEnemyState
{
    private float originalSpeed;
    private readonly NonAggressiveView _view;
    
    public NonAggressiveRunState(NonAggressiveEnemy enemy, EnemyStateMachine nonAggressiveEnemyStateMachine) : base(enemy, nonAggressiveEnemyStateMachine)
    {
        _view = enemy.NonAggressiveView;
    }

    public override void EnterState()
    {
        base.EnterState();

        var speed = enemy.NavMeshAgent.speed;
        originalSpeed = speed;
        speed *= 2;
        enemy.NavMeshAgent.speed = speed;
        _view.StartRunning();
    }

    public override void ExitState()
    {
        base.ExitState();
        
        enemy.NavMeshAgent.speed = originalSpeed;
        _view.StopPlayback();
        
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        if (enemy.NearByPotentialAttackers.Count == 0)
        {
            nonAggressiveEnemyStateMachine.SwitchState(enemy.IdleState);
        }
        else
        {
            var position = enemy.transform.position;
            Vector3 directionToClosestAttacker = (enemy.NearByPotentialAttackers[0].transform.position - position);
        
            Vector3 runToPoint = position - directionToClosestAttacker.normalized * enemy.NavMeshAgent.speed;

            enemy.NavMeshAgent.SetDestination(runToPoint);
        }
    }
}
