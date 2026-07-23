using UnityEngine;

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
        return baseUnitCapacity;
    }

    public int GetUnitCount()
    {
        return army.peasantCount + army.infantryCount + army.rangedCount + army.cavalryCount;
    }
}
