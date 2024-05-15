using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAggressiveStateMachine 
{
    public NonAggressiveEnemyState CurrentNonAggressiveEnemyState { get; set; }

    public void Initialize(NonAggressiveEnemyState startingState)
    {
        CurrentNonAggressiveEnemyState = startingState;
        CurrentNonAggressiveEnemyState.EnterState();
    }

    public void SwitchState(NonAggressiveEnemyState newState)
    {
        CurrentNonAggressiveEnemyState.ExitState();
        CurrentNonAggressiveEnemyState = newState;
        CurrentNonAggressiveEnemyState.EnterState();
    }
}
