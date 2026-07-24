using UnityEngine;

public class Peasant : Unit
{
    [SerializeField] AudioClip[] attackAudio;
    public override void Attack()
    {
        animator.Play("PeasantAttack");
    }
    public override ArmyManager.UnitType GetUnitType()
    {
        return ArmyManager.UnitType.PEASANT;
    }
    public override void OnHitEnemy(Unit target)
    {
        base.OnHitEnemy(target);
        AudioManager.Instance.PlayRandomSFX(attackAudio, transform, 0.4f);
    }
}
