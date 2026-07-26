using UnityEngine;

public class RegionManager : MonoBehaviour
{
    [SerializeField]
    Region[] regions;

    public static RegionManager Instance;

    [SerializeField]
    RegionInitializer[] regionInitializers;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        regions = new Region[regionInitializers.Length];

        for (int i = 0; i < regionInitializers.Length; i++)
        {
            RegionInitializer initializer = regionInitializers[i];
            Region newRegion = new Region(initializer.regionNumber, initializer.regionName, initializer.isRegionOccupied, initializer.regionIncome, initializer.regionFood, initializer.buildingSlots, initializer.regionArmy, initializer.hasViscount);

            regions[i] = newRegion;
        }

        for (int i = 0; i < regionInitializers.Length; i++)
        {
            RegionInitializer initializer = regionInitializers[i];
            Region currentRegion = regions[i];

            Region[] neighborRegions = new Region[initializer.neighborRegions.Length];

            for (int j = 0; j < initializer.neighborRegions.Length; j++)
            {
                neighborRegions[j] = regions[initializer.neighborRegions[j]];
            }

            currentRegion.SetRegionNeighbors(neighborRegions);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Region GetRegion(int index)
    {
        return regions[index];
    }

    public Region[] GetAllRegions()
    {
        return regions;
    }
}
