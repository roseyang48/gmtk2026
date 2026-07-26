using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    RegionUIController regionUIController;

    [SerializeField]
    UnitBuildController unitBuildController;

    [SerializeField]
    Building[] buildingOptions;

    [SerializeField]
    string[] regionObjectNames;

    RegionController[] regionControllers;

    [SerializeField] DialogueObject tutorialDialogue;

    int targetRegion = -1;

    int turnCount = 1;

    bool hasAttacked = false;
    public bool onUnitScreen;

    int techBuildingsBuilt = 0;
    float techBuildingCostMod = 1.25f;

    public static GameManager Instance;

    [SerializeField] private Transform armyPopupTransform;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
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
        if(PlayerPrefs.GetInt("TutorialSeen") != 1)
        {
            DialogueHandler.instance.TriggerDialogue(tutorialDialogue);
            PlayerPrefs.SetInt("TutorialSeen", 1);
        }

        int occupiedCount = 0;

        for (int i = 0; i < RegionManager.Instance.GetAllRegions().Length; i++)
        {
            if (RegionManager.Instance.GetRegion(i).IsRegionOccupied())
            {
                occupiedCount += 1;
            }
        }
        CountCounterAnimation.Instance.InitializeCounter(occupiedCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegionSelected(int regionNumber)
    {
        regionUIController.ShowCanvas(RegionManager.Instance.GetRegion(regionNumber));
        unitBuildController.HideCanvas();

        foreach (RegionController region in regionControllers)
        {
            if (region.GetRegionNumber() == regionNumber)
            {
                region.ShowSelectSprite();
            }
            else
            {
                region.HideSelectSprite();
            }
        }
    }

    public void UnitUpgradeSelected()
    {
        unitBuildController.ShowCanvas();
        regionUIController.HideCanvas();

        foreach (RegionController region in regionControllers)
        {
            region.HideSelectSprite();
        }
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
        foreach (RegionController region in regionControllers)
        {
            region.HideSelectSprite();
        }
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
                foodIncome += currentRegion.GetRegionFood();

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

    public void RegionIncomePopups()
    {
        Region[] regions = RegionManager.Instance.GetAllRegions();
        for(int i = 0; i < regions.Length; i++)
        {
            Region currRegion = regions[i];
            if(!currRegion.IsRegionOccupied())
            {
                int totalGold = currRegion.GetRegionIncome();
                int totalFood = currRegion.GetRegionFood();
                int totalWood = 0;
                foreach(Building currBuilding in currRegion.GetConstructedBuildings())
                {
                    if(currBuilding != null)
                    {
                        totalGold -= currBuilding.GetGoldUpkeep();

                        totalGold += currBuilding.GetGoldIncome();
                        totalFood += currBuilding.GetFoodIncome();
                        totalWood += currBuilding.GetWoodIncome();
                    }
                }
                StartCoroutine(SpawnSequentialPopups(totalGold, totalFood, totalWood, i));
            }
        }
    }

    private IEnumerator SpawnSequentialPopups(int gold, int food, int wood, int regionNumber)
    {
        if(gold != 0)
        {
            PopUpHandler.Instance.SpawnPopup((gold > 0 ? "+" : "") + gold + " <sprite=\"final_icons\" index=4>!", GetRegionTransform(regionNumber).position);
            yield return new WaitForSeconds(0.5f);
        }
        if(food != 0)
        {
            PopUpHandler.Instance.SpawnPopup((food > 0 ? "+" : "") + food + " <sprite=\"final_icons\" index=5>!", GetRegionTransform(regionNumber).position);
            yield return new WaitForSeconds(0.5f);
        }
        if(wood != 0)
        {
            PopUpHandler.Instance.SpawnPopup((wood > 0 ? "+" : "") + wood + " <sprite=\"final_icons\" index=6>!", GetRegionTransform(regionNumber).position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UnitUpkeepPopups()
    {
        StartCoroutine(SpawnSequentialUnitPopups());
    }
    private IEnumerator SpawnSequentialUnitPopups()
    {
        int peasantUpkeep = ArmyManager.Instance.GetFoodUpkeep(ArmyManager.UnitType.PEASANT);
        int infantryUpkeep = ArmyManager.Instance.GetFoodUpkeep(ArmyManager.UnitType.INFANTRY);
        int rangedUpkeep = ArmyManager.Instance.GetFoodUpkeep(ArmyManager.UnitType.RANGED);
        int cavalryUpkeep = ArmyManager.Instance.GetFoodUpkeep(ArmyManager.UnitType.CAVALRY);
        if(peasantUpkeep > 0)
        {
            PopUpHandler.Instance.SpawnPopup(ArmyManager.Instance.GetArmy().peasantCount + "x <sprite=\"final_icons\" index=0>: -" + peasantUpkeep + " <sprite=\"final_icons\" index=5>!", armyPopupTransform.position);
            yield return new WaitForSeconds(0.5f);
        }
        if(infantryUpkeep > 0)
        {
            PopUpHandler.Instance.SpawnPopup(ArmyManager.Instance.GetArmy().infantryCount + "x <sprite=\"final_icons\" index=1>: -" + infantryUpkeep + " <sprite=\"final_icons\" index=5>!", armyPopupTransform.position);
            yield return new WaitForSeconds(0.5f);
        }
        if(rangedUpkeep > 0)
        {
            PopUpHandler.Instance.SpawnPopup(ArmyManager.Instance.GetArmy().rangedCount + "x <sprite=\"final_icons\" index=2>: -" + rangedUpkeep + " <sprite=\"final_icons\" index=5>!", armyPopupTransform.position);
            yield return new WaitForSeconds(0.5f);
        }
        if(cavalryUpkeep > 0)
        {
            PopUpHandler.Instance.SpawnPopup(ArmyManager.Instance.GetArmy().cavalryCount + "x <sprite=\"final_icons\" index=3>: -" + cavalryUpkeep + " <sprite=\"final_icons\" index=5>!", armyPopupTransform.position);
            yield return new WaitForSeconds(0.5f);
        }
    } 

    public void AdvanceTurn()
    {
        unitBuildController.HideCanvas();
        regionUIController.HideCanvas();

        foreach (RegionController region in regionControllers)
        {
            region.HideSelectSprite();
        }

        TransitionHandler.Instance.Transition();
    }

    public void StartNewTurn()
    {
        turnCount += 1;

        int[] upkeep = ComputeUpkeep();
        RegionIncomePopups();
        UnitUpkeepPopups();
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
            SceneSwitcher.Instance.LoadScene(SceneSwitcher.SceneType.VICTORY, turnCount - 1);
            Destroy(gameObject);
        }

        // Check for loss
        if (ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) < 0
            || ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD) < 0)
        {
            string cause;

            if (ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) < 0 && ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD) < 0)
            {
                cause = "You ran out of food and gold.";
            }
            else if (ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD) < 0)
            {
                cause = "You ran out of food.";
            }
            else
            {
                cause = "You ran out of gold.";
            }

            SceneSwitcher.Instance.LoadScene(SceneSwitcher.SceneType.DEFEAT, cause);
            Destroy(gameObject);
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
        int occupiedCount = 0;

        for (int i = 0; i < RegionManager.Instance.GetAllRegions().Length; i++)
        {
            if (RegionManager.Instance.GetRegion(i).IsRegionOccupied())
            {
                occupiedCount += 1;
            }
        }
        CountCounterAnimation.Instance.InitializeCounter(occupiedCount);

        if (wasSuccessful)
        {
            RegionManager.Instance.GetRegion(targetRegion).ChangeRegionStatus(false);
            PopUpHandler.Instance.SpawnPopup("Count Down!", GetRegionTransform(targetRegion).position);
            CountCounterAnimation.Instance.TickDown();
        }
        targetRegion = -1;
    }

    public void UpdateUIReferences()
    {
        regionUIController = GameObject.Find("RegionUICanvas").GetComponent<RegionUIController>();
        unitBuildController = GameObject.Find("UnitBuildCanvas").GetComponent<UnitBuildController>();

        regionControllers = new RegionController[RegionManager.Instance.GetAllRegions().Length];

        for (int i = 0; i < RegionManager.Instance.GetAllRegions().Length; i++)
        {
            regionControllers[i] = GameObject.Find("GameMap/" + regionObjectNames[i]).GetComponent<RegionController>();
        }
    }

    public bool HasAttacked()
    {
        return hasAttacked;
    }

    public Transform GetRegionTransform(int regionNumber)
    {
        return regionControllers[regionNumber].GetPopupTransform();
    }

    public int GetTechBuildingsBuilt()
    {
        return techBuildingsBuilt;
    }

    public void IncrementTechBuildingCount()
    {
        techBuildingsBuilt++;
    }

    public float GetTechBuildingCostMod()
    {
        return techBuildingCostMod;
    }
}
