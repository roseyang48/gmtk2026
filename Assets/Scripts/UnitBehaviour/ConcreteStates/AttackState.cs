using System.Threading;
using UnityEngine;

public class AttackState : UnitState
{
    private float attackTimer = 0f;
    public AttackState(Unit unit, UnitStateMachine stateMachine) : base(unit, stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        unit.rb.MoveRotation(Vector2.SignedAngle(Vector2.up, unit.targetUnit.transform.position - unit.transform.position));
        unit.SetObstacle(true);
        attackTimer += Random.Range(unit.statBlock.attackInterval * 0.8f, unit.statBlock.attackInterval);
    }

    public override void ExitState()
    {
        base.ExitState();
        unit.SetObstacle(false);
    }
    public override void Update()
    {
        base.Update();
        if(unit.targetUnit == null)
        {
            unit.StateMachine.ChangeState(unit.IdleState);
        }
        else if((unit.transform.position - unit.targetUnit.transform.position).magnitude > unit.statBlock.attackRange)
        {
            unit.StateMachine.ChangeState(unit.ChaseState);
        }
        else
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= unit.statBlock.attackInterval)
            {
                unit.Attack();
                attackTimer = 0f;
            }
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void AnimationTriggerEvent(Unit.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
        if(type == Unit.AnimationTriggerType.Hit)
        {
            unit.OnHitEnemy(unit.targetUnit);
        }
    }
}
