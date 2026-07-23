using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Building", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    [SerializeField]
    string buildingName;

    public string GetBuildingName()
    {
        return buildingName;
    }

    [SerializeField]
    [TextArea(4, 100)]
    string buildingDescription;

    public string GetBuildingDescription()
    {
        return buildingDescription;
    }

    [SerializeField]
    int woodCost;

    public int GetWoodCost()
    {
        return woodCost;
    }

    [SerializeField]
    int goldCost;

    public int GetGoldCost()
    {
        return goldCost;
    }

    [SerializeField]
    int goldUpkeep;

    public int GetGoldUpkeep()
    {
        return goldUpkeep;
    }

    [SerializeField]
    int goldIncome;

    public int GetGoldIncome()
    {
        return goldIncome;
    }

    [SerializeField]
    int foodIncome;

    public int GetFoodIncome()
    {
        return foodIncome;
    }

    [SerializeField]
    int woodIncome;

    public int GetWoodIncome()
    {
        return woodIncome;
    }


    [SerializeField]
    int unitCapacityBonus;

    public int GetUnitCapacityBonus()
    {
        return unitCapacityBonus;
    }


}
