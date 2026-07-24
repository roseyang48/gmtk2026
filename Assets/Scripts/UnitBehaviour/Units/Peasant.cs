using UnityEngine;

public class Peasant : Unit
{
    public override void Attack()
    {
        animator.Play("PeasantAttack");
    }
    public override ArmyManager.UnitType GetUnitType()
    {
        return ArmyManager.UnitType.PEASANT;
    }
}
