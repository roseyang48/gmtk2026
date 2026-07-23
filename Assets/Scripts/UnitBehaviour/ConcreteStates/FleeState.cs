using UnityEngine;

public class FleeState : UnitState
{
    public FleeState(Unit unit, UnitStateMachine stateMachine) : base(unit, stateMachine)
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
        if(unit.unitTeam == Unit.Team.Player)
        {
            Vector2 targetPos = new Vector2(unit.transform.position.x, GameObject.FindGameObjectWithTag("PlayerFleePoint").transform.position.y);
            unit.SetTarget(targetPos);
        }
        else
        {
            Vector2 targetPos = new Vector2(unit.transform.position.x, GameObject.FindGameObjectWithTag("EnemyFleePoint").transform.position.y);
            unit.SetTarget(targetPos);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
