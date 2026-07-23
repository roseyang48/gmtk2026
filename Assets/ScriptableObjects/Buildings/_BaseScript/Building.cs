using UnityEngine;

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
}
