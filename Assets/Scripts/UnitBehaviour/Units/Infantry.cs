using UnityEngine;

public class Infantry : Unit
{
    [SerializeField] AudioClip[] attackAudio;
    [SerializeField] float upgradeDamageMod;
    [SerializeField] float upgradeDamageReduction;
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
    public override void ChangeHP(float value)
    {
        if(ArmyManager.Instance.CheckCombatFlag("infantryUpgraded0") && unitTeam == CombatHandler.Team.Player)
        {
            base.ChangeHP(Mathf.Min(0, value * upgradeDamageMod + upgradeDamageReduction));
        }
        else
        {
            base.ChangeHP(value);
        }
    }
}
