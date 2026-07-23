using UnityEngine;

[CreateAssetMenu(fileName = "StatBlock", menuName = "Scriptable Objects/StatBlock")]
public class StatBlock : ScriptableObject
{
    public float maxHP;
    public float attackInterval;
    public float attackDamage;
    public float attackRange;
    public float moveSpeed;
    public float rotationSpeed;
    public float fleeThreshhold;
    public float fleeChance;
}
