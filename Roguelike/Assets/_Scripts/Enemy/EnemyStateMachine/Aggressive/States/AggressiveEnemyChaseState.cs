using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggressiveEnemyChaseState : AggressiveEnemyState
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly RobotView _robotView;
    private float _timePassed;
    

    public AggressiveEnemyChaseState(AggressiveEnemy aggressiveEnemy, AggressiveEnemyStateMachine aggressiveEnemyStateMachine) : base(aggressiveEnemy, aggressiveEnemyStateMachine)
    {
        _navMeshAgent = aggressiveEnemy.NavMeshAgent;
        if (aggressiveEnemy is Robot robot)
        {
            _robotView = robot.RobotView;
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _robotView.Run(_navMeshAgent.velocity.magnitude, _navMeshAgent.speed);

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
            _navMeshAgent.SetDestination(closestTarget.position);
            aggressiveEnemy.transform.LookAt(closestTarget);

            if (_timePassed >= aggressiveEnemy.AttackCooldown)
            {
                if (closestDistance <= aggressiveEnemy.AttackRange)
                {
                    aggressiveEnemy.StateMachine.SwitchState(aggressiveEnemy.AttackState);
                    _timePassed = 0;
                }
                else if (closestDistance > aggressiveEnemy.AttackRange + 1.0f)
                {
                    aggressiveEnemy.StateMachine.SwitchState(aggressiveEnemy.IdleState);
                }
            }
        }

        _timePassed += Time.deltaTime;
    }
}