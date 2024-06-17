using UnityEngine;
using UnityEngine.AI;

public class NonAggressiveIdleState : NonAggressiveEnemyState
{
    private const float Range = 30f;
    private Transform _centrePoint;
    private float _nextMoveTime;

    const float MinBtwnMoves = 3.0f;

    private readonly NavMeshAgent _navMeshAgent;
    private readonly NonAggressiveView _view;

    public NonAggressiveIdleState(NonAggressiveEnemy enemy, EnemyStateMachine nonAggressiveEnemyStateMachine) :
        base(enemy, nonAggressiveEnemyStateMachine)
    {
        _navMeshAgent = enemy.NavMeshAgent;
        _view = enemy.NonAggressiveView;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        if (_navMeshAgent.hasPath)
            _view.StartWalking();
        else
            _view.StartIdling();

        if (enemy.IsGroup)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                enemy.NavMeshAgent.SetDestination(enemy.Point);
            }
        }
        else
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (enemy.RandomPoint(enemy.transform.position, Range, out var point) && PassedTime(_nextMoveTime))
                {
                    _nextMoveTime = SetFutureRandomTime(MinBtwnMoves, MinBtwnMoves * 2);
                    enemy.NavMeshAgent.SetDestination(point);
                }
            }
        }
    }

    private bool PassedTime(float time) => Time.time >= time;
    private float SetFutureRandomTime(float min, float max) => Time.time + Random.Range(min, max);
}