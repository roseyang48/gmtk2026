using UnityEngine;

public class Archer : Unit
{
    [SerializeField] GameObject arrow;
    [SerializeField] AudioClip[] attackAudio;
    [SerializeField] float dmgUpgradeValue;
    [SerializeField] float rangeUpgradeValue;
    [SerializeField] Transform gunBarrelTransform;
    public override void Initialize(Color hatColor, CombatHandler.Team team)
    {
        base.Initialize(hatColor, team);
        if(ArmyManager.Instance.CheckCombatFlag("rangedUpgraded0") && team == CombatHandler.Team.Player)
        {
            statBlock.attackDamage += dmgUpgradeValue;
            statBlock.attackRange += rangeUpgradeValue;
        }
    }
    public override void Attack()
    {
        animator.Play("ArcherAttack");
    }
    public override ArmyManager.UnitType GetUnitType()
    {
        return ArmyManager.UnitType.RANGED;
    }
    public override void OnHitEnemy(Unit target)
    {
        Vector2 targetPos = target.transform.position;
        rb.MoveRotation(Mathf.Atan2(targetPos.y - gunBarrelTransform.position.y, targetPos.x - gunBarrelTransform.position.x)*Mathf.Rad2Deg - 90);
        AudioManager.Instance.PlayRandomSFX(attackAudio, transform, 0.4f);
        GameObject arrowInstance = Instantiate(arrow, gunBarrelTransform.position, Quaternion.identity);
        arrowInstance.GetComponent<Rigidbody2D>().MoveRotation(Mathf.Atan2(targetPos.y - gunBarrelTransform.position.y, targetPos.x - gunBarrelTransform.position.x)*Mathf.Rad2Deg + 90);
        arrowInstance.GetComponent<ArrowScript>().Initialize(unitTeam,
            (target.transform.position - gunBarrelTransform.position).normalized * statBlock.projectileSpeed,
            statBlock.attackDamage);
    }
}
