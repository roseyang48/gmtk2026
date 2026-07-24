using UnityEngine;

public class ChaseState : UnitState
{
    public ChaseState(Unit unit, UnitStateMachine stateMachine) : base(unit, stateMachine)
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
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(unit.UpdateTarget())
        {
            if(unit.targetUnit != null)
            {
                unit.SetTarget(unit.targetUnit.transform);
                if((unit.transform.position - unit.targetUnit.transform.position).magnitude <= unit.statBlock.attackRange)
                {
                    unit.StateMachine.ChangeState(unit.AttackState);
                }
            }
            else
            {
                unit.StateMachine.ChangeState(unit.IdleState);
            }
        }
        else
        {
            unit.SetTarget(null);
            unit.StateMachine.ChangeState(unit.IdleState);
        }
    }
}
