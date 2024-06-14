using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAggressiveEnemyState: IEnemyState
{
    protected readonly NonAggressiveEnemy enemy;
    protected readonly EnemyStateMachine nonAggressiveEnemyStateMachine;

    protected NonAggressiveEnemyState(NonAggressiveEnemy enemy, EnemyStateMachine nonAggressiveEnemyStateMachine)
    {
        this.enemy = enemy;
        this.nonAggressiveEnemyStateMachine = nonAggressiveEnemyStateMachine;
    }
    
    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void FrameUpdate() {}
    public void FixedUpdate() {}
}
