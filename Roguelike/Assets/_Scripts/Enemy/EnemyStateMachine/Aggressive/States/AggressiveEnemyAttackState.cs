using UnityEngine;

public class AggressiveEnemyAttackState : AggressiveEnemyState
{
    private readonly RobotView _robotView;
    
    public AggressiveEnemyAttackState(AggressiveEnemy aggressiveEnemy, AggressiveEnemyStateMachine aggressiveEnemyStateMachine) : base(aggressiveEnemy, aggressiveEnemyStateMachine)
    {
        if (aggressiveEnemy is Robot robot)
        {
            _robotView = robot.RobotView;
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _robotView.Attack();

        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (var target in aggressiveEnemy.Targets)
        {
            float distanceToTarget = Vector3.Distance(aggressiveEnemy.transform.position, target.position);
            
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = target;
            }
        }

        if (closestTarget != null)
        {
            if (closestDistance > aggressiveEnemy.AttackRange)
            {
                aggressiveEnemy.StateMachine.SwitchState(aggressiveEnemy.IdleState);
            }
        }
    }
}
