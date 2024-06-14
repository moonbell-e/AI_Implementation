using UnityEngine;
using UnityEngine.AI;

public class AggressiveEnemyIdleState : AggressiveEnemyState
{
    private float _timePassed;
    private readonly RobotView _robotView;
    private const float Range = 30f;
    private Transform _centrePoint;
    private readonly NavMeshAgent _navMeshAgent;


    public AggressiveEnemyIdleState(AggressiveEnemy aggressiveEnemy) : base(aggressiveEnemy)
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
            if (closestDistance <= aggressiveEnemy.AggroRange)
            {
                aggressiveEnemy.StateMachine.SwitchState(aggressiveEnemy.ChaseState);
            }
            else
            {
                _robotView.Walk();

                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (RandomPoint(aggressiveEnemy.transform.position, Range, out var point))
                    {
                        aggressiveEnemy.NavMeshAgent.SetDestination(point);
                    }
                }
            }
        }
    } 

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}