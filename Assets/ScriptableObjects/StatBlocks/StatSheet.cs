using UnityEngine;

public class StatSheet
{
    public StatSheet(StatBlock baseBlock)
    {
        maxHP = baseBlock.maxHP;
        attackInterval = baseBlock.attackInterval;
        attackDamage = baseBlock.attackDamage;
        attackRange = baseBlock.attackRange;
        moveSpeed = baseBlock.moveSpeed;
        rotationSpeed = baseBlock.rotationSpeed;
        fleeThreshhold = baseBlock.fleeThreshhold;
        fleeChance = baseBlock.fleeChance;
        projectileSpeed = baseBlock.projectileSpeed;
    }
    public float maxHP;
    public float attackInterval;
    public float attackDamage;
    public float attackRange;
    public float moveSpeed;
    public float rotationSpeed;
    public float fleeThreshhold;
    public float fleeChance;
    [Header("Archer Stats")]
    public float projectileSpeed;
}
