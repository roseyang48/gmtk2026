using UnityEngine;
using TMPro;
using System.Collections;

public class ResourceCanvasController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI goldCounter;
    [SerializeField]
    TextMeshProUGUI goldTrendCounter;

    [SerializeField]
    TextMeshProUGUI woodCounter;
    [SerializeField]
    TextMeshProUGUI woodTrendCounter;

    [SerializeField]
    TextMeshProUGUI foodCounter;
    [SerializeField]
    TextMeshProUGUI foodTrendCounter;

    [SerializeField]
    TextMeshProUGUI unitCapacityCounter;

    [SerializeField]
    TextMeshProUGUI unit0Counter;

    [SerializeField]
    TextMeshProUGUI unit1Counter;

    [SerializeField]
    TextMeshProUGUI unit2Counter;

    [SerializeField]
    TextMeshProUGUI unit3Counter;

    [SerializeField]
    TextMeshProUGUI unit4Counter;


    int goldDisplayedValue = -1;
    int foodDisplayedValue = -1;
    int woodDisplayedValue = -1;

    float targetTickerTime = .25f;

    Coroutine goldTickerCoroutine;
    Coroutine foodTickerCoroutine;
    Coroutine woodTickerCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goldDisplayedValue = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD);
        foodDisplayedValue = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD);
        woodDisplayedValue = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.WOOD);

        ResourceManager.Instance.GetResourceChangedEvent().AddListener(OnResourceChanged);
    }

    // Update is called once per frame
    void Update()
    {
        goldCounter.text = goldDisplayedValue.ToString();
        foodCounter.text = foodDisplayedValue.ToString();
        woodCounter.text = woodDisplayedValue.ToString();

        int[] buildingUpkeep = GameManager.Instance.ComputeUpkeep();

        goldTrendCounter.text = "(" + (buildingUpkeep[0] > 0 ? "+" : "") + buildingUpkeep[0].ToString() + ")";
        foodTrendCounter.text = "(" + (buildingUpkeep[1] > 0 ? "+" : "") + buildingUpkeep[1].ToString() + ")";
        woodTrendCounter.text = "(" + (buildingUpkeep[2] > 0 ? "+" : "") + buildingUpkeep[2].ToString() + ")";

        unitCapacityCounter.text = ArmyManager.Instance.GetUnitCount() + "/" + ArmyManager.Instance.GetUnitCapacity();

        unit0Counter.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.PEASANT).ToString();
        unit1Counter.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.INFANTRY).ToString();
        unit2Counter.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.RANGED).ToString();
        unit3Counter.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.CAVALRY).ToString();
    }

    void OnResourceChanged(ResourceManager.ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceManager.ResourceType.GOLD:
                if (goldTickerCoroutine != null)
                {
                    StopCoroutine(goldTickerCoroutine);
                }
                goldTickerCoroutine = StartCoroutine(GoldTicker());
                break;
            case ResourceManager.ResourceType.FOOD:
                if (foodTickerCoroutine != null)
                {
                    StopCoroutine(foodTickerCoroutine);
                }
                foodTickerCoroutine = StartCoroutine(FoodTicker());
                break;
            case ResourceManager.ResourceType.WOOD:
                if (woodTickerCoroutine != null)
                {
                    StopCoroutine(woodTickerCoroutine);
                }
                woodTickerCoroutine = StartCoroutine(WoodTicker());
                break;
        }
    }

    IEnumerator GoldTicker()
    {
        while (goldDisplayedValue != ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD))
        {
            if (goldDisplayedValue > ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD))
            {
                goldDisplayedValue -= 1;
            }
            else
            {
                goldDisplayedValue += 1;
            }
            
            yield return new WaitForSeconds(targetTickerTime / Mathf.Abs(goldDisplayedValue - ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD)));
        }
    }

    IEnumerator FoodTicker()
    {
        while (foodDisplayedValue != ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD))
        {
            if (foodDisplayedValue > ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD))
            {
                foodDisplayedValue -= 1;
            }
            else
            {
                foodDisplayedValue += 1;
            }

            yield return new WaitForSeconds(targetTickerTime / Mathf.Abs(foodDisplayedValue - ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD)));
        }
    }

    IEnumerator WoodTicker()
    {
        while (woodDisplayedValue != ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.WOOD))
        {
            if (woodDisplayedValue > ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.WOOD))
            {
                woodDisplayedValue -= 1;
            }
            else
            {
                woodDisplayedValue += 1;
            }

            yield return new WaitForSeconds(targetTickerTime / Mathf.Abs(woodDisplayedValue - ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.WOOD)));
        }
    }
}
