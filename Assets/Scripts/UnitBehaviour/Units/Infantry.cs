using UnityEngine;

public class Infantry : Unit
{
    [SerializeField] AudioClip[] attackAudio;
    public override void Attack()
    {
        animator.Play("InfantryAttack");
    }
    public override ArmyManager.UnitType GetUnitType()
    {
        return ArmyManager.UnitType.INFANTRY;
    }
    public override void OnHitEnemy(Unit target)
    {
        base.OnHitEnemy(target);
        AudioManager.Instance.PlayRandomSFX(attackAudio, transform, 0.4f);
    }
}
