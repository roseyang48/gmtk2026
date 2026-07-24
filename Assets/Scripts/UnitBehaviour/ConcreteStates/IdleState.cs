using UnityEngine;

public class IdleState : UnitState
{
    private bool survivorCounted;
    public IdleState(Unit unit, UnitStateMachine stateMachine) : base(unit, stateMachine)
    {
        
    }
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
    public override void Update()
    {
        base.Update();
        bool enemiesPresent = unit.UpdateTarget();
        if(enemiesPresent)
        {
            unit.StateMachine.ChangeState(unit.ChaseState);
        }
        else
        {
            if(!survivorCounted)
            {
                survivorCounted = true;
                CombatHandler.Instance.EndCombat(unit.unitTeam, unit.GetUnitType());
            }
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
