using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegionUIController : MonoBehaviour
{
    [SerializeField]
    CanvasGroup mainCanvasGroup;

    [SerializeField]
    RectTransform mainRectTransform;

    [SerializeField]
    CanvasGroup buildCanvasGroup;

    [SerializeField]
    RectTransform buildRectTransform;


    [SerializeField]
    RectTransform buildingTransform;

    [SerializeField]
    RectTransform armyTransform;

    [SerializeField]
    TextMeshProUGUI regionNameLabel;

    [SerializeField]
    TextMeshProUGUI occupationLabel;

    [SerializeField]
    BuildingButtonController[] buildingButtons;
    [SerializeField]
    TextMeshProUGUI[] unitCountLabels;


    [SerializeField]
    TextMeshProUGUI buildingName;

    [SerializeField]
    TextMeshProUGUI buildingDescription;

    [SerializeField]
    TextMeshProUGUI buildingGoldCost;

    [SerializeField]
    TextMeshProUGUI buildingWoodCost;

    [SerializeField]
    Button confirmButton;


    [SerializeField]
    Button assaultButton;

    [SerializeField]
    TextMeshProUGUI assaultButtonText;


    int activeBuildingSlot;
    Building selectedBuilding;

    Region activeRegion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCanvas(Region region)
    {
        mainCanvasGroup.alpha = 1f;
        mainCanvasGroup.interactable = true;
        mainCanvasGroup.blocksRaycasts = true;

        buildingTransform.gameObject.SetActive(!region.IsRegionOccupied());
        armyTransform.gameObject.SetActive(region.IsRegionOccupied());

        regionNameLabel.text = region.GetRegionName();
        occupationLabel.text = region.IsRegionOccupied() ? "OCCUPIED" : "LIBERATED";

        bool neighborLiberated = false;

        for (int i = 0; i < region.GetNeighborRegions().Length; i++)
        {
            if (!region.GetNeighborRegions()[i].IsRegionOccupied())
            {
                neighborLiberated = true;
                break;
            }
        }

        if (region.IsRegionOccupied())
        {
            HideBuildMenu();
            assaultButton.gameObject.SetActive(true);

            if (GameManager.Instance.HasAttacked())
            {
                assaultButton.interactable = false;
                assaultButtonText.text = "Already Attacked!";
            }
            else if (neighborLiberated)
            {
                assaultButton.interactable = true;
                assaultButtonText.text = "Begin Assault!";
            }
            else
            {
                assaultButton.interactable = false;
                assaultButtonText.text = "No Route";
            }

            unitCountLabels[0].text = region.GetRegionArmy().peasantCount.ToString();
            unitCountLabels[1].text = region.GetRegionArmy().infantryCount.ToString();
            unitCountLabels[2].text = region.GetRegionArmy().rangedCount.ToString();
            unitCountLabels[3].text = region.GetRegionArmy().cavalryCount.ToString();
        }
        else
        {
            assaultButton.gameObject.SetActive(false);
        }

        activeRegion = region;

        RedrawBuildings();
    }

    public void HideCanvas()
    {
        mainCanvasGroup.alpha = 0f;
        mainCanvasGroup.interactable = false;
        mainCanvasGroup.blocksRaycasts = false;

        activeRegion = null;

        HideBuildMenu();
    }

    public void ShowBuildMenu(int buildingSlot)
    {
        buildCanvasGroup.alpha = 1f;
        buildCanvasGroup.interactable = true;
        buildCanvasGroup.blocksRaycasts = true;

        buildRectTransform.sizeDelta = new Vector2(buildRectTransform.sizeDelta.x, mainRectTransform.sizeDelta.y);

        activeBuildingSlot = buildingSlot;

        DrawBuildingInfo(null);
    }

    public void HideBuildMenu()
    {
        buildCanvasGroup.alpha = 0f;
        buildCanvasGroup.interactable = false;
        buildCanvasGroup.blocksRaycasts = false;

        for (int i = 0; i < buildingButtons.Length; i++)
        {
            buildingButtons[i].GetComponent<CanvasGroup>().alpha = 0;
            buildingButtons[i].GetComponent<CanvasGroup>().interactable = false;
            buildingButtons[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void DrawBuildingInfo(Building building)
    {
        if (building == null)
        {
            buildingName.text = "None";
            buildingDescription.text = "";
            buildingGoldCost.text = "--- Gold";
            buildingWoodCost.text = "--- Wood";
            confirmButton.interactable = false;
            return;
        }

        selectedBuilding = building;

        buildingName.text = building.GetBuildingName();
        buildingDescription.text = building.GetBuildingDescription();
        buildingGoldCost.text = building.GetGoldCost() + " Gold";
        buildingWoodCost.text = building.GetWoodCost() + " Wood";

        if (building.GetGoldCost() > ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) || building.GetWoodCost() > ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.WOOD))
        {
            confirmButton.interactable = false;
        }//Can add more conditionals here
        else
        {
            confirmButton.interactable = true;
        }
    }

    public void ConfirmBuilding()
    {
        activeRegion.ConstructBuilding(selectedBuilding, activeBuildingSlot);

        ResourceManager.Instance.SpendResource(ResourceManager.ResourceType.GOLD, selectedBuilding.GetGoldCost());
        ResourceManager.Instance.SpendResource(ResourceManager.ResourceType.WOOD, selectedBuilding.GetWoodCost());

        HideBuildMenu();
        RedrawBuildings();
    }

    public void BeginAssault()
    {
        GameManager.Instance.BeginAssault(activeRegion.GetRegionNumber());
    }

    void RedrawBuildings()
    {
        for (int i = 0; i < buildingButtons.Length; i++)
        {
            if (activeRegion.GetBuildingSlots() > i)
            {
                buildingButtons[i].GetComponent<CanvasGroup>().alpha = 1;
                buildingButtons[i].GetComponent<CanvasGroup>().interactable = true;
                buildingButtons[i].GetComponent<CanvasGroup>().blocksRaycasts = true;

                buildingButtons[i].SetBuilding(activeRegion.GetConstructedBuildings()[i]);
            }
        }
    }
}
