using UnityEngine;

[CreateAssetMenu(fileName = "New Region", menuName = "Scriptable Objects/Region")]
[System.Serializable]
class RegionInitializer : ScriptableObject
{
    public int regionNumber;
    public string regionName;
    public bool isRegionOccupied;
    public bool hasViscount;
    public int regionIncome;
    public int buildingSlots;
    public Army regionArmy;

    public int[] neighborRegions;
}