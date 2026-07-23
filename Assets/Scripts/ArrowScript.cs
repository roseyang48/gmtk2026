using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Unit.Team owner;
    private float damage;
    [SerializeField] private Rigidbody2D rb;
    public void Initialize(Unit.Team owner, Vector3 velocity, float damage)
    {
        this.owner = owner;
        rb.linearVelocity = velocity;
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch(owner)
        {
            case Unit.Team.Player:
                if(collision.tag == "EnemyUnit")
                {
                    collision.GetComponent<Unit>().ChangeHP(-damage);
                    Destroy(gameObject);
                }
                break;
            case Unit.Team.Enemy:
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
