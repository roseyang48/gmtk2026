using UnityEngine;
using TMPro;

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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldCounter.text = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD).ToString();
        woodCounter.text = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.WOOD).ToString();
        foodCounter.text = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD).ToString();

        int[] buildingUpkeep = GameManager.Instance.ComputeUpkeep();

        goldTrendCounter.text = (buildingUpkeep[0] > 0 ? "+" : "") + buildingUpkeep[0].ToString();
        foodTrendCounter.text = (buildingUpkeep[1] > 0 ? "+" : "") + buildingUpkeep[1].ToString();
        woodTrendCounter.text = (buildingUpkeep[2] > 0 ? "+" : "") + buildingUpkeep[2].ToString();

        unitCapacityCounter.text = ArmyManager.Instance.GetUnitCount() + "/" + ArmyManager.Instance.GetUnitCapacity();

        unit0Counter.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.PEASANT).ToString();
        unit1Counter.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.INFANTRY).ToString();
        unit2Counter.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.RANGED).ToString();
        unit3Counter.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.CAVALRY).ToString();
    }
}
