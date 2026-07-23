using NUnit.Framework.Internal;
using UnityEngine;

public class ARMYTEST : MonoBehaviour
{
    [SerializeField] private Army testPlayerArmy;
    [SerializeField] private Army testEnemyArmy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CombatHandler.Instance.Initialize(testPlayerArmy, testEnemyArmy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
