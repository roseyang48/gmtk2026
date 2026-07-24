using NavMeshPlus.Extensions;
using NUnit.Framework;
using UnityEngine;

public class Cavalry : Unit
{
    [SerializeField] private float chargeTimer;
    [SerializeField] private float chargeDamageMod;
    [SerializeField] private TrailRenderer chargeTrailRenderer;
    [SerializeField] private float minSpeed;
    private bool isCharging = false;
    private float timer = 0f;
    [SerializeField] AudioClip[] attackAudio;
    public override void Attack()
    {
        if(!isCharging)
        {
            animator.Play("CavalryAttack");
            timer = 0f;
        }
        else
        {
            animator.Play("CavalryCharge");
            timer = 0f;
        }
    }
    public override void Update()
    {
        base.Update();
        if(StateMachine.currState == ChaseState && !isCharging)
        {
            timer += Time.deltaTime;
            if(timer >= chargeTimer)
            {
                isCharging = true;
                chargeTrailRenderer.emitting = true;
                timer = 0f;
            }
        }
        if(isCharging && agent.velocity.magnitude <= minSpeed)
        {
            isCharging = false;
            chargeTrailRenderer.emitting = false;
        }
    }
    public override ArmyManager.UnitType GetUnitType()
    {
        return ArmyManager.UnitType.CAVALRY;
    }
    public override void OnHitEnemy(Unit target)
    {
        AudioManager.Instance.PlayRandomSFX(attackAudio, transform, 0.4f);
        if(!isCharging)
        {
            base.OnHitEnemy(target);
        }
        else
        {
            target.ChangeHP(-statBlock.attackDamage * chargeDamageMod);
            isCharging = false;
            chargeTrailRenderer.emitting = false;
        }
    }
}
