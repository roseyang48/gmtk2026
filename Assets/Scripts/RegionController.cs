using UnityEngine;

public class RegionController : MonoBehaviour
{
    [SerializeField]
    int regionNumber = -1;

    [SerializeField]
    string regionName;

    [SerializeField]
    bool isRegionOccupied;

    [SerializeField]
    RegionController[] neighborRegions;

    [SerializeField]
    int buildingSlots = 2;

    [SerializeField]
    Building[] constructedBuildings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        constructedBuildings = new Building[buildingSlots];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsOnScreenLeft()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        return screenPos.x < Camera.main.scaledPixelWidth / 2;
    }

    public int GetRegionNumber()
    {
        return regionNumber;
    }

    public string GetRegionName()
    {
        return regionName;
    }

    public bool IsRegionOccupied()
    {
        return isRegionOccupied;
    }

    public int GetBuildingSlots()
    {
        return buildingSlots;
    }

    public Building[] GetConstructedBuildings()
    {
        return constructedBuildings;
    }
}
