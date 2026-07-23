using UnityEngine;

public class UnitState
{
    protected Unit unit;
    protected UnitStateMachine stateMachine;

    public UnitState(Unit unit, UnitStateMachine stateMachine)
    {
        this.unit = unit;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState()
    {
        
    }

    public virtual void ExitState()
    {
        
    }
    public virtual void Update()
    {
        
    }
    public virtual void FixedUpdate()
    {
        
    }
    public virtual void AnimationTriggerEvent(Unit.AnimationTriggerType type)
    {
        
    }
}
