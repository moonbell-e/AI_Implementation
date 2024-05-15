using UnityEngine;
using UnityEngine.AI;

public class NonAggressiveIdleState: NonAggressiveEnemyState
{
    private const float Range = 30f;
    private Transform _centrePoint;
    private float _nextMoveTime;
    
    const float MinBtwnMoves = 3.0f;
    
    private readonly NavMeshAgent _navMeshAgent;
    
    public NonAggressiveIdleState(NonAggressiveEnemy enemy, NonAggressiveStateMachine nonAggressiveEnemyStateMachine) : base(enemy, nonAggressiveEnemyStateMachine)
    {
        _navMeshAgent = enemy.NavMeshAgent;
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

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (RandomPoint(enemy.transform.position, Range, out var point) && PassedTime(_nextMoveTime))
            {
                _nextMoveTime = SetFutureRandomTime(MinBtwnMoves, MinBtwnMoves * 2);
                enemy.NavMeshAgent.SetDestination(point);
            }
        }
    }
    
    private bool PassedTime(float time) => Time.time >= time;
    private float SetFutureRandomTime(float min, float max) => Time.time + Random.Range(min, max);
}
