using UnityEngine;

public class NonAggressiveRunState : NonAggressiveEnemyState
{
    private float originalSpeed;
    
    public NonAggressiveRunState(NonAggressiveEnemy enemy, NonAggressiveStateMachine nonAggressiveEnemyStateMachine) : base(enemy, nonAggressiveEnemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        var speed = enemy.NavMeshAgent.speed;
        originalSpeed = speed;
        speed *= 2;
        enemy.NavMeshAgent.speed = speed;
    }

    public override void ExitState()
    {
        base.ExitState();
        
        enemy.NavMeshAgent.speed = originalSpeed;
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
