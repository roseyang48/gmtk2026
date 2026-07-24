using UnityEngine;
using System.Collections.Generic;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public enum UnitType
    {
        PEASANT,
        INFANTRY,
        RANGED,
        CAVALRY,
    }

    Dictionary<UnitType, int> foodUpkeep = new Dictionary<UnitType, int>()
    {
        {UnitType.PEASANT, 3},
        {UnitType.INFANTRY, 6},
        {UnitType.RANGED, 6},
        {UnitType.CAVALRY, 6},
    };

    Dictionary<UnitType, int> buildCostGold = new Dictionary<UnitType, int>()
    {
        {UnitType.PEASANT, 50},
        {UnitType.INFANTRY, 100},
        {UnitType.RANGED, 150},
        {UnitType.CAVALRY, 200},
    };

    Dictionary<UnitType, int> buildCostFood = new Dictionary<UnitType, int>()
    {
        {UnitType.PEASANT, 100},
        {UnitType.INFANTRY, 150},
        {UnitType.RANGED, 100},
        {UnitType.CAVALRY, 200},
    };

    [SerializeField]
    Army army;

    int baseUnitCapacity = 10;

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
    }

    public void BuildNewUnit(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.PEASANT:
                army.peasantCount += 1;
                break;
            case UnitType.INFANTRY:
                army.peasantCount -= 1;
                army.infantryCount += 1;
                break;
            case UnitType.RANGED:
                army.peasantCount -= 1;
                army.rangedCount += 1;
                break;
            case UnitType.CAVALRY:
                army.peasantCount -= 1;
                army.cavalryCount += 1;
                break;
            default:
                Debug.LogError("Invalid Unit Type");
                Debug.Break();
                break;
        }
    }

    public void LoseUnits(UnitType unitType, int amount)
    {
        switch (unitType)
        {
            case UnitType.PEASANT:
                army.peasantCount -= amount;
                break;
            case UnitType.INFANTRY:
                army.infantryCount -= amount;
                break;
            case UnitType.RANGED:
                army.rangedCount -= amount;
                break;
            case UnitType.CAVALRY:
                army.cavalryCount -= amount;
                break;
            default:
                Debug.LogError("Invalid Unit Type");
                Debug.Break();
                break;
        }
    }

    public int GetUnitCount(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.PEASANT:
                return army.peasantCount;
            case UnitType.INFANTRY:
                return army.infantryCount;
            case UnitType.RANGED:
                return army.rangedCount;
            case UnitType.CAVALRY:
                return army.cavalryCount;
            default:
                Debug.LogError("Invalid Unit Type");
                Debug.Break();
                return -1;
        }
    }

    public int GetUnitCapacity()
    {
        int bonusUnitCapacity = 0;

        Building[] buildings = GameManager.Instance.GetConstructedBuildings(false);
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] != null)
            {
                bonusUnitCapacity += buildings[i].GetUnitCapacityBonus();
            }
        }

        return baseUnitCapacity + bonusUnitCapacity;
    }

    public int GetUnitCount()
    {
        return army.GetArmySize();
    }

    public int GetFoodUpkeep(UnitType unitType)
    {
        return foodUpkeep[unitType] * GetUnitCount(unitType);
    }

    public int GetGoldBuildCost(UnitType unitType)
    {
        return buildCostGold[unitType];
    }

    public int GetFoodBuildCost(UnitType unitType)
    {
        return buildCostFood[unitType];
    }

    public bool CanBuildUnitType(UnitType unitType)
    {
        if (unitType == UnitType.PEASANT) { return true; }

        Building[] buildings = GameManager.Instance.GetConstructedBuildings(false);
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] != null)
            {
                switch (unitType)
                {
                    case UnitType.PEASANT:
                        return true;
                    case UnitType.INFANTRY:
                        if (buildings[i].IsInfantryUnlocker()) { return true; }
                        break;
                    case UnitType.RANGED:
                        if (buildings[i].IsRangedUnlocker()) { return true; }
                        break;
                    case UnitType.CAVALRY:
                        if (buildings[i].IsCavalryUnlocker()) { return true; }
                        break;
                }
            }
        }

        return false;
    }

    public Army GetArmy()
    {
        return army;
    }

    public void SetSurvivingUnits(UnitType unitType, int amount)
    {
        switch (unitType)
        {
            case UnitType.PEASANT:
                army.peasantCount += amount;
                break;
            case UnitType.INFANTRY:
                army.infantryCount += amount;
                break;
            case UnitType.RANGED:
                army.rangedCount += amount;
                break;
            case UnitType.CAVALRY:
                army.cavalryCount += amount;
                break;
            default:
                Debug.LogError("Invalid Unit Type");
                Debug.Break();
                break;
        }
    }

    public void DispatchUnits(UnitType unitType, int amount)
    {
        switch (unitType)
        {
            case UnitType.PEASANT:
                army.peasantCount -= amount;
                break;
            case UnitType.INFANTRY:
                army.infantryCount -= amount;
                break;
            case UnitType.RANGED:
                army.rangedCount -= amount;
                break;
            case UnitType.CAVALRY:
                army.cavalryCount -= amount;
                break;
            default:
                Debug.LogError("Invalid Unit Type");
                Debug.Break();
                break;
        }
    }
}
