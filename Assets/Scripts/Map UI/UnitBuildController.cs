using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitBuildController : MonoBehaviour
{
    [SerializeField]
    CanvasGroup unitCanvasGroup;

    [SerializeField]
    TextMeshProUGUI unitCapacityText;


    [SerializeField]
    TextMeshProUGUI peasantCapacityText;
    
    [SerializeField]
    Button peasantBuildButton;

    [SerializeField]
    TextMeshProUGUI peasantBuildTextMain;

    [SerializeField]
    TextMeshProUGUI peasantBuildTextSecondary;



    [SerializeField]
    TextMeshProUGUI infantryCapacityText;

    [SerializeField]
    Button infantryBuildButton;

    [SerializeField]
    TextMeshProUGUI infantryBuildTextMain;

    [SerializeField]
    TextMeshProUGUI infantryBuildTextSecondary;



    [SerializeField]
    TextMeshProUGUI rangedCapacityText;

    [SerializeField]
    Button rangedBuildButton;

    [SerializeField]
    TextMeshProUGUI rangedBuildTextMain;

    [SerializeField]
    TextMeshProUGUI rangedBuildTextSecondary;



    [SerializeField]
    TextMeshProUGUI cavalaryCapacityText;

    [SerializeField]
    Button cavalryBuildButton;

    [SerializeField]
    TextMeshProUGUI cavalryBuildTextMain;

    [SerializeField]
    TextMeshProUGUI cavalryBuildTextSecondary;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        unitCapacityText.text = "Unit Capacity: " + ArmyManager.Instance.GetUnitCount() + "/" + ArmyManager.Instance.GetUnitCapacity();

        bool canBuildPeasant = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD) >= ArmyManager.Instance.GetFoodBuildCost(ArmyManager.UnitType.PEASANT)
            && ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) >= ArmyManager.Instance.GetGoldBuildCost(ArmyManager.UnitType.PEASANT)
            && ArmyManager.Instance.GetUnitCount() < ArmyManager.Instance.GetUnitCapacity();

        peasantCapacityText.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.PEASANT).ToString();
        peasantBuildButton.interactable = canBuildPeasant;
        peasantBuildTextMain.text = canBuildPeasant ? "Recruit" : "Unavailable";
        peasantBuildTextSecondary.text = ArmyManager.Instance.GetGoldBuildCost(ArmyManager.UnitType.PEASANT) + " Gold, " + ArmyManager.Instance.GetFoodBuildCost(ArmyManager.UnitType.PEASANT) + " Food";

        bool canBuildInfantry = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD) >= ArmyManager.Instance.GetFoodBuildCost(ArmyManager.UnitType.INFANTRY)
            && ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) >= ArmyManager.Instance.GetGoldBuildCost(ArmyManager.UnitType.INFANTRY)
            && ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.PEASANT) >= 1
            && ArmyManager.Instance.CanBuildUnitType(ArmyManager.UnitType.INFANTRY);

        infantryCapacityText.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.INFANTRY).ToString();
        infantryBuildButton.interactable = canBuildInfantry;
        if (ArmyManager.Instance.CanBuildUnitType(ArmyManager.UnitType.INFANTRY))
        {
            infantryBuildTextMain.text = canBuildInfantry ? "Upgrade" : "Unavailable";
        }
        else
        {
            infantryBuildTextMain.text = "Requres Blacksmith";
        }
        infantryBuildTextSecondary.text = ArmyManager.Instance.GetGoldBuildCost(ArmyManager.UnitType.INFANTRY) + " Gold, " + ArmyManager.Instance.GetFoodBuildCost(ArmyManager.UnitType.INFANTRY) + " Food";

        bool canBuildRanged = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD) >= ArmyManager.Instance.GetFoodBuildCost(ArmyManager.UnitType.RANGED)
            && ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) >= ArmyManager.Instance.GetGoldBuildCost(ArmyManager.UnitType.RANGED)
            && ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.PEASANT) >= 1
            && ArmyManager.Instance.CanBuildUnitType(ArmyManager.UnitType.RANGED);

        rangedCapacityText.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.RANGED).ToString();
        rangedBuildButton.interactable = canBuildRanged;
        if (ArmyManager.Instance.CanBuildUnitType(ArmyManager.UnitType.RANGED))
        {
            rangedBuildTextMain.text = canBuildRanged ? "Upgrade" : "Unavailable";
        }
        else
        {
            rangedBuildTextMain.text = "Requires Gunsmith";
        }
        rangedBuildTextSecondary.text = ArmyManager.Instance.GetGoldBuildCost(ArmyManager.UnitType.RANGED) + " Gold, " + ArmyManager.Instance.GetFoodBuildCost(ArmyManager.UnitType.RANGED) + " Food";

        bool canBuildCavalry = ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.FOOD) >= ArmyManager.Instance.GetFoodBuildCost(ArmyManager.UnitType.CAVALRY)
            && ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) >= ArmyManager.Instance.GetGoldBuildCost(ArmyManager.UnitType.CAVALRY)
            && ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.PEASANT) >= 1
            && ArmyManager.Instance.CanBuildUnitType(ArmyManager.UnitType.CAVALRY);

        cavalaryCapacityText.text = ArmyManager.Instance.GetUnitCount(ArmyManager.UnitType.CAVALRY).ToString();
        cavalryBuildButton.interactable = canBuildCavalry;
        if (ArmyManager.Instance.CanBuildUnitType(ArmyManager.UnitType.CAVALRY))
        {
            cavalryBuildTextMain.text = canBuildCavalry ? "Upgrade" : "Unavailable";
        }
        else
        {
            cavalryBuildTextMain.text = "Requires Stables";
        }
        cavalryBuildTextSecondary.text = ArmyManager.Instance.GetGoldBuildCost(ArmyManager.UnitType.CAVALRY) + " Gold, " + ArmyManager.Instance.GetFoodBuildCost(ArmyManager.UnitType.CAVALRY) + " Food";

    }

    public void ShowCanvas()
    {
        unitCanvasGroup.alpha = 1f;
        unitCanvasGroup.interactable = true;
        unitCanvasGroup.blocksRaycasts = true;
    }

    public void HideCanvas()
    {
        unitCanvasGroup.alpha = 0f;
        unitCanvasGroup.interactable = false;
        unitCanvasGroup.blocksRaycasts = false;
    }
}
