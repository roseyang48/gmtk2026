using NUnit.Framework.Internal;
using UnityEngine;

public class ARMYTEST : MonoBehaviour
{
    [SerializeField] private Army testPlayerArmy;
    [SerializeField] private Army testEnemyArmy;
    [SerializeField] private bool peasantUpgraded;
    [SerializeField] private bool rangedUpgraded;
    [SerializeField] private bool cavalryUpgraded;
    [SerializeField] private bool infantryUpgraded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CombatHandler.Instance.Initialize(testPlayerArmy, testEnemyArmy);
        ArmyManager.Instance.SetCombatFlag("peasantUpgraded0", peasantUpgraded);
        ArmyManager.Instance.SetCombatFlag("rangedUpgraded0", rangedUpgraded);
        ArmyManager.Instance.SetCombatFlag("infantryUpgraded0", infantryUpgraded);
        ArmyManager.Instance.SetCombatFlag("cavalryUpgraded0", cavalryUpgraded);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
