using UnityEngine;

public class Peasant : Unit
{
    [SerializeField] private AudioClip[] attackAudio;
    [SerializeField] private float upgradeBonusMinHP;
    [SerializeField] private float upgradeMaxDamageRes;
    public override void Attack()
    {
        animator.Play("PeasantAttack");
    }
    public override void Update()
    {
        base.Update();
        if(ArmyManager.Instance.CheckCombatFlag("peasantUpgraded0") && unitTeam == CombatHandler.Team.Player)
        {
            statBlock.attackInterval = baseStats.attackInterval*(currHP/statBlock.maxHP*(1-upgradeBonusMinHP) + upgradeBonusMinHP);
        }
    }
    public override void CheckFlee()
    {
        if(!ArmyManager.Instance.CheckCombatFlag("peasantUpgraded0") || unitTeam != CombatHandler.Team.Player)
        {
            base.CheckFlee();
        }
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
    public override void ChangeHP(float value)
    {
        if(ArmyManager.Instance.CheckCombatFlag("peasantUpgraded0") && unitTeam == CombatHandler.Team.Player)
        {
            base.ChangeHP(Mathf.Min(0, value * (1 - upgradeMaxDamageRes * Mathf.Min(1, (statBlock.maxHP - currHP)/(statBlock.maxHP - upgradeBonusMinHP)))));
        }
        else
        {
            base.ChangeHP(value);
        }
    }
}
