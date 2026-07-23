using System.Collections;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public enum Team {Player, Enemy}
    public void Initialize(Army playerArmy, Army enemyArmy)
    {
        
    }

    private IEnumerator SpawnPlayerArmy()
    {
        yield return null;
    }
}
