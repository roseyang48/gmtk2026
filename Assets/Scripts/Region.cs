using UnityEngine;

public class Region
{
    [SerializeField]
    int regionNumber = -1;

    [SerializeField]
    string regionName;

    [SerializeField]
    bool isRegionOccupied;

    [SerializeField]
    Region[] neighborRegions;

    [SerializeField]
    int regionIncome = 0;

    [SerializeField]
    int buildingSlots = 2;

    [SerializeField]
    Building[] constructedBuildings;

    [SerializeField]
    Army regionArmy;

    public Region(int regionNumber, string regionName, bool isRegionOccupied, int regionIncome, int buildingSlots, Army regionArmy)
    {
        this.regionNumber = regionNumber;
        this.regionName = regionName;
        this.isRegionOccupied = isRegionOccupied;
        this.regionIncome = regionIncome;
        this.buildingSlots = buildingSlots;
        constructedBuildings = new Building[buildingSlots];
        this.regionArmy = regionArmy;
    }

    public void SetRegionNeighbors(Region[] neighborRegions)
    {
        this.neighborRegions = neighborRegions;
    }

    public string GetRegionName()
    {
        return regionName;
    }

    public bool IsRegionOccupied()
    {
        return isRegionOccupied;
    }

    public Region[] GetNeighborRegions()
    {
        return neighborRegions;
    }

    public int GetRegionIncome()
    {
        return regionIncome;
    }

    public int GetBuildingSlots()
    {
        return buildingSlots;
    }

    public Building[] GetConstructedBuildings()
    {
        return constructedBuildings;
    }

    public void ConstructBuilding(Building building, int slot)
    {
        constructedBuildings[slot] = building;
    }

    public Army GetRegionArmy()
    {
        return regionArmy;
    }

    public void ChangeRegionStatus(bool isOccupied)
    {
        isRegionOccupied = isOccupied;
    }

    public int GetRegionNumber()
    {
        return regionNumber;
    }
}
