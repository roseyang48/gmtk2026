using Unity.VisualScripting;
using UnityEngine;

public class UnitStateMachine
{
    public UnitState currState;

    public void Initialize(UnitState startingState)
    {
        currState = startingState;
        currState.EnterState();
    }

    public void ChangeState(UnitState newState)
    {
        currState.ExitState();
        currState = newState;
        currState.EnterState();
    }
}
