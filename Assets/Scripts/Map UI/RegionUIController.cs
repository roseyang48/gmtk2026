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
    Image buildingSprite;

    [SerializeField]
    TextMeshProUGUI buildingDescription;

    [SerializeField]
    TextMeshProUGUI buildingGoldCost;

    [SerializeField]
    TextMeshProUGUI buildingWoodCost;

    [SerializeField]
    TextMeshProUGUI buildingUpkeep;

    [SerializeField]
    TextMeshProUGUI buildingTags;

    [SerializeField]
    Button confirmButton;


    [SerializeField]
    Button assaultButton;

    [SerializeField]
    TextMeshProUGUI assaultButtonText;


    [SerializeField]
    TextMeshProUGUI regionGoldText;

    [SerializeField]
    TextMeshProUGUI regionFoodText;

    [SerializeField]
    BuildOptionController[] buildOptionControllers;

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
        occupationLabel.text = region.IsRegionOccupied() ? "OCCUPIED BY " + (region.HasViscount() ? "VISCOUNT" : "COUNT") : "LIBERATED";

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

        regionGoldText.text = "<sprite=\"final_icons\" index=4>: " + region.GetRegionIncome();
        regionFoodText.text = "<sprite=\"final_icons\" index=5>: " + region.GetRegionFood();

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

        foreach (BuildOptionController buildOption in buildOptionControllers)
        {
            bool missingPrereqs = false;

            foreach (string flag in buildOption.GetBuildingOption().GetPrereqFlags())
            {
                if (!ArmyManager.Instance.CheckCombatFlag(flag))
                {
                    missingPrereqs = true;
                    break;
                }
            }

            if (missingPrereqs)
            {
                buildOption.gameObject.SetActive(false);
                continue;
            }

            if (buildOption.GetBuildingOption().IsUnique())
            {
                bool validBuild = true;
                foreach (string flag in buildOption.GetBuildingOption().GetArmyFlags())
                {
                    if (ArmyManager.Instance.CheckCombatFlag(flag))
                    {
                        validBuild = false;
                        break;
                    }
                }
                buildOption.gameObject.SetActive(validBuild);
            }
            else
            {
                buildOption.gameObject.SetActive(true);
            }
        }
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
            buildingSprite.sprite = null;
            buildingDescription.text = "";
            buildingGoldCost.text = "--- <sprite=\"final_icons\" index=4>";
            buildingWoodCost.text = "--- <sprite=\"final_icons\" index=6>";
            confirmButton.interactable = false;
            return;
        }

        selectedBuilding = building;

        int goldCost = building.GetGoldCost();
        int woodCost = building.GetWoodCost();

        if (building.IsTech())
        {
            goldCost = Mathf.FloorToInt(goldCost * Mathf.Pow(GameManager.Instance.GetTechBuildingCostMod(), GameManager.Instance.GetTechBuildingsBuilt()));
            woodCost = Mathf.FloorToInt(woodCost * Mathf.Pow(GameManager.Instance.GetTechBuildingCostMod(), GameManager.Instance.GetTechBuildingsBuilt()));
        }

        buildingName.text = building.GetBuildingName();
        buildingSprite.sprite = building.GetBuildingIcon();
        buildingDescription.text = building.GetBuildingDescription();
        buildingGoldCost.text = goldCost + " <sprite=\"final_icons\" index=4>";
        buildingWoodCost.text = woodCost + " <sprite=\"final_icons\" index=6>";
        buildingUpkeep.text = "Upkeep: " + building.GetGoldUpkeep() + " <sprite=\"final_icons\" index=4>";
        string tags = (building.IsTech() ? "Tech" : "") + (building.IsTech() && building.IsUnique() ? ", " : "") + (building.IsUnique() ? "Unique" : "");
        buildingTags.text = tags;

        if (goldCost > ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.GOLD) || woodCost > ResourceManager.Instance.GetResourceCount(ResourceManager.ResourceType.WOOD))
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

        foreach (string flag in selectedBuilding.GetArmyFlags())
        {
            ArmyManager.Instance.SetCombatFlag(flag, true);
        }

        if (selectedBuilding.IsTech())
        {
            GameManager.Instance.IncrementTechBuildingCount();
        }

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
            else
            {
                buildingButtons[i].GetComponent<CanvasGroup>().alpha = 0;
                buildingButtons[i].GetComponent<CanvasGroup>().interactable = false;
                buildingButtons[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }
}
