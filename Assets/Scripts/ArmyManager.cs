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

    int peasantCount = 3;

    int infantryCount = 2;

    int rangedCount = 1;

    int cavalryCount = 0;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildNewUnit(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.PEASANT:
                peasantCount += 1;
                break;
            case UnitType.INFANTRY:
                peasantCount -= 1;
                infantryCount += 1;
                break;
            case UnitType.RANGED:
                peasantCount -= 1;
                rangedCount += 1;
                break;
            case UnitType.CAVALRY:
                peasantCount -= 1;
                cavalryCount += 1;
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
                peasantCount -= amount;
                break;
            case UnitType.INFANTRY:
                infantryCount -= amount;
                break;
            case UnitType.RANGED:
                rangedCount -= amount;
                break;
            case UnitType.CAVALRY:
                cavalryCount -= amount;
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
                return peasantCount;
            case UnitType.INFANTRY:
                return infantryCount;
            case UnitType.RANGED:
                return rangedCount;
            case UnitType.CAVALRY:
                return cavalryCount;
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
        return peasantCount + infantryCount + rangedCount + cavalryCount;
    }
}
