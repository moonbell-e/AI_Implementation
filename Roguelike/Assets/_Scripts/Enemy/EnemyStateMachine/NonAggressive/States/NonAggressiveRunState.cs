using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAggressiveRunState : NonAggressiveEnemyState
{
    public NonAggressiveRunState(NonAggressiveEnemy enemy, NonAggressiveStateMachine nonAggressiveEnemyStateMachine) : base(enemy, nonAggressiveEnemyStateMachine)
    {
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        var position = enemy.transform.position;
        Vector3 normDir = (enemy.PotentialAttacker.transform.position - position).normalized;

        normDir = Quaternion.AngleAxis(Random.Range(0, 179), Vector3.up) * normDir;

        enemy.NavMeshAgent.SetDestination(position - (normDir * 100f));
    }
}
