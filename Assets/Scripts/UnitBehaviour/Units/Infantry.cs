using UnityEngine;

public class Infantry : Unit
{
    public override void Attack()
    {
        animator.Play("InfantryAttack");
    }
    public override ArmyManager.UnitType GetUnitType()
    {
        return ArmyManager.UnitType.INFANTRY;
    }
}
