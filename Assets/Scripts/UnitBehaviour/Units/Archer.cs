using UnityEngine;

public class Archer : Unit
{
    [SerializeField] GameObject arrow;
    [SerializeField] AudioClip[] attackAudio;
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
        AudioManager.Instance.PlayRandomSFX(attackAudio, transform, 0.4f);
        GameObject arrowInstance = Instantiate(arrow, transform.position, Quaternion.identity);
        arrowInstance.GetComponent<Rigidbody2D>().MoveRotation(Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x)*Mathf.Rad2Deg + 90);
        arrowInstance.GetComponent<ArrowScript>().Initialize(unitTeam,
            (target.transform.position - transform.position).normalized * statBlock.projectileSpeed,
            statBlock.attackDamage);
    }
}
