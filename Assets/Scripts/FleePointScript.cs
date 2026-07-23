using UnityEngine;

public class FleePointScript : MonoBehaviour
{
    [SerializeField] private Unit.Team team;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Unit collisionUnit = collision.GetComponent<Unit>();
        if(collisionUnit.StateMachine.currState == collisionUnit.FleeState)
        {
            switch(team)
            {
                case Unit.Team.Player:
                    if(collision.gameObject.tag == "PlayerUnit")
                    {
                        //MAKE SURE TO DO THE THING THAT INCREMENTS SURVIVORS :D
                        Destroy(collision.gameObject);
                    }
                    break;
                case Unit.Team.Enemy:
                    if(collision.gameObject.tag == "EnemyUnit")
                    {
                        //DO IT HERE TOO :3
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
