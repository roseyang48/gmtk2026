using UnityEngine;

public class FleePointScript : MonoBehaviour
{
    [SerializeField] private CombatHandler.Team team;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Unit collisionUnit = collision.GetComponent<Unit>();
        if(collisionUnit != null)
        {
            if(collisionUnit.StateMachine.currState == collisionUnit.FleeState)
            {
                switch(team)
                {
                    case CombatHandler.Team.Player:
                        if(collision.gameObject.tag == "PlayerUnit")
                        {
                            CombatHandler.Instance.AddSurvivor(collisionUnit.GetUnitType());
                            Destroy(collision.gameObject);
                        }
                        break;
                    case CombatHandler.Team.Enemy:
                        if(collision.gameObject.tag == "EnemyUnit")
                        {
                            //NO ENEMY SURVIVORS
                            Destroy(collision.gameObject);
                        }
                        break;
                    default:
                        Debug.LogError("somoething that's funky ended up fleeing????");
                        break;
                }
            }
        }
    }
}
