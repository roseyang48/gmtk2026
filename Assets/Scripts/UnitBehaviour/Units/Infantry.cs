using UnityEngine;

public class Infantry : Unit
{
    public override void Attack()
    {
        animator.Play("InfantryAttack");
    }
}
