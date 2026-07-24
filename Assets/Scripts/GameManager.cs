using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    RegionUIController regionUIController;

    [SerializeField]
    UnitBuildController unitBuildController;

    [SerializeField]
    Building[] buildingOptions;

    int targetRegion = -1;

    int turnCount = 1;

    bool hasAttacked = false;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    public void RegionSelected(int regionNumber)
    {
        regionUIController.ShowCanvas(RegionManager.Instance.GetRegion(regionNumber));
        unitBuildController.HideCanvas();
    }

    public void UnitUpgradeSelected()
    {
        unitBuildController.ShowCanvas();
        regionUIController.HideCanvas();
    }

    public void BuildingSelected(int buildingSlot)
    {
        regionUIController.ShowBuildMenu(buildingSlot);
    }

    public void BuildingOptionSelected(Building buildingOption)
    {
        regionUIController.DrawBuildingInfo(buildingOption);
    }

    public void BuildUnit(ArmyManager.UnitType unitType)
    {
        ArmyManager.Instance.BuildNewUnit(unitType);
        ResourceManager.Instance.SpendResource(ResourceManager.ResourceType.GOLD, ArmyManager.Instance.GetGoldBuildCost(unitType));
        ResourceManager.Instance.SpendResource(ResourceManager.ResourceType.FOOD, ArmyManager.Instance.GetFoodBuildCost(unitType));
    }

    public Building[] GetBuildingOptions()
    {
        return buildingOptions;
    }

    public void CancelAction()
    {
        regionUIController.HideCanvas();
        unitBuildController.HideCanvas();
    }

    public Building[] GetConstructedBuildings(bool includeOccupied)
    {
        List<Building> buildings = new List<Building>();

        Region[] regions = RegionManager.Instance.GetAllRegions();

        for (int i = 0; i < regions.Length; i++)
        {
            if (!regions[i].IsRegionOccupied() || includeOccupied)
            {
                buildings.AddRange(regions[i].GetConstructedBuildings());
            }
        }

        return buildings.ToArray();
    }

    public int GetTurnCount()
    {
        return turnCount;
    }

    public int[] ComputeUpkeep()
    {
        int goldIncome = 0;

        int woodIncome = 0;

        int foodIncome = 0;

        Region[] regions = RegionManager.Instance.GetAllRegions();

        for (int i = 0; i < regions.Length; i++)
        {
            Region currentRegion = regions[i];
            if (!currentRegion.IsRegionOccupied())
            {
                goldIncome += currentRegion.GetRegionIncome();

                for (int j = 0; j < currentRegion.GetBuildingSlots(); j++)
                {
                    Building currentBuilding = currentRegion.GetConstructedBuildings()[j];
                    if (currentBuilding != null)
                    {
                        goldIncome -= currentBuilding.GetGoldUpkeep();

                        goldIncome += currentBuilding.GetGoldIncome();
                        foodIncome += currentBuilding.GetFoodIncome();
                        woodIncome += currentBuilding.GetWoodIncome();
                    }
                }
            }
        }

        foodIncome -= ArmyManager.Instance.GetFoodUpkeep(ArmyManager.UnitType.PEASANT);
        foodIncome -= ArmyManager.Instance.GetFoodUpkeep(ArmyManager.UnitType.INFANTRY);
        foodIncome -= ArmyManager.Instance.GetFoodUpkeep(ArmyManager.UnitType.RANGED);
        foodIncome -= ArmyManager.Instance.GetFoodUpkeep(ArmyManager.UnitType.CAVALRY);

        return new int[]{goldIncome, foodIncome, woodIncome};
    }

    public void AdvanceTurn()
    {
        unitBuildController.HideCanvas();
        regionUIController.HideCanvas();

        turnCount += 1;

        int[] upkeep = ComputeUpkeep();

        if (upkeep[0] > 0)
        {
            ResourceManager.Instance.GainResource(ResourceManager.ResourceType.GOLD, upkeep[0]);
        }
        else if (upkeep[0] < 0)
        {
            ResourceManager.Instance.SpendResource(ResourceManager.ResourceType.GOLD, upkeep[0] * -1);
        }

        if (upkeep[1] > 0)
        {
            ResourceManager.Instance.GainResource(ResourceManager.ResourceType.FOOD, upkeep[1]);
        }
        else if (upkeep[1] < 0)
        {
            ResourceManager.Instance.SpendResource(ResourceManager.ResourceType.FOOD, upkeep[1] * -1);
        }

        if (upkeep[2] > 0)
        {
            ResourceManager.Instance.GainResource(ResourceManager.ResourceType.WOOD, upkeep[2]);
        }
        else if (upkeep[2] < 0)
        {
            ResourceManager.Instance.SpendResource(ResourceManager.ResourceType.WOOD, upkeep[2] * -1);
        }

        // Check for win
        bool stillOccupied = false;

        for (int i = 0; i < RegionManager.Instance.GetAllRegions().Length; i++)
        {
            if (RegionManager.Instance.GetRegion(i).IsRegionOccupied())
            {
                stillOccupied = true;
                break;
            }
        }

        if (!stillOccupied)
        {
            SceneSwitcher.Instance.LoadScene(SceneSwitcher.SceneType.VICTORY);
        }

        // Check for loss
        if (ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) < 0
            || ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD) < 0)
        {
            SceneSwitcher.Instance.LoadScene(SceneSwitcher.SceneType.DEFEAT);
        }

        hasAttacked = false;
    }

    public void BeginAssault(int regionNumber)
    {
        targetRegion = regionNumber;

        Army dispatchArmy = new Army
        {
            peasantCount = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.PEASANT),
            infantryCount = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.INFANTRY),
            rangedCount = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.RANGED),
            cavalryCount = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.CAVALRY)
        };

        ArmyManager.Instance.DispatchUnits(ArmyManager.UnitType.PEASANT, ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.PEASANT));
        ArmyManager.Instance.DispatchUnits(ArmyManager.UnitType.INFANTRY, ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.INFANTRY));
        ArmyManager.Instance.DispatchUnits(ArmyManager.UnitType.RANGED, ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.RANGED));
        ArmyManager.Instance.DispatchUnits(ArmyManager.UnitType.CAVALRY, ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.CAVALRY));

        hasAttacked = true;

        SceneSwitcher.Instance.LoadScene(SceneSwitcher.SceneType.COMBAT, dispatchArmy, RegionManager.Instance.GetRegion(regionNumber).GetRegionArmy());
    }

    public void ConcludeAssault(bool wasSuccessful)
    {
        if (wasSuccessful)
        {
            RegionManager.Instance.GetRegion(targetRegion).ChangeRegionStatus(false);
        }

        targetRegion = -1;
    }

    public void UpdateUIReferences()
    {
        regionUIController = GameObject.Find("RegionUICanvas").GetComponent<RegionUIController>();
        unitBuildController = GameObject.Find("UnitBuildCanvas").GetComponent<UnitBuildController>();
    }

    public bool HasAttacked()
    {
        return hasAttacked;
    }
}
