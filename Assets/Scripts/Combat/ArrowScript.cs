using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private CombatHandler.Team owner;
    private float damage;
    [SerializeField] private Rigidbody2D rb;
    public void Initialize(CombatHandler.Team owner, Vector3 velocity, float damage)
    {
        this.owner = owner;
        rb.linearVelocity = velocity;
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch(owner)
        {
            case CombatHandler.Team.Player:
                if(collision.tag == "EnemyUnit")
                {
                    collision.GetComponent<Unit>().ChangeHP(-damage);
                    Destroy(gameObject);
                }
                break;
            case CombatHandler.Team.Enemy:
                if(collision.tag == "PlayerUnit")
                {
                    collision.GetComponent<Unit>().ChangeHP(-damage);
                    Destroy(gameObject);
                }
                break;
            default:
                Debug.LogError("Unrecognized tag of object hit by arrow");
                break;
        }
    }
}
