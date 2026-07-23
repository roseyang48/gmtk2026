using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceType
    {
        GOLD,
        FOOD,
        WOOD,
    }

    public static ResourceManager Instance;

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

    int goldCount = 100;
    int foodCount = 100;
    int woodCount = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetResourceCount(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.GOLD:
                return goldCount;
            case ResourceType.FOOD:
                return foodCount;
            case ResourceType.WOOD:
                return woodCount;
            default:
                Debug.LogError("Invalid Resource Type");
                Debug.Break();
                return -1;
        }
    }

    // Returns amount left?
    public int SpendResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.GOLD:
                goldCount -= amount;
                return goldCount;
            case ResourceType.FOOD:
                foodCount -= amount;
                return foodCount;
            case ResourceType.WOOD:
                woodCount -= amount;
                return woodCount;
            default:
                Debug.LogError("Invalid Resource Type");
                Debug.Break();
                return -1;
        }
    }

    // Returns amount left?
    public int GainResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.GOLD:
                goldCount += amount;
                return goldCount;
            case ResourceType.FOOD:
                foodCount += amount;
                return foodCount;
            case ResourceType.WOOD:
                woodCount += amount;
                return woodCount;
            default:
                Debug.LogError("Invalid Resource Type");
                Debug.Break();
                return -1;
        }
    }
}
