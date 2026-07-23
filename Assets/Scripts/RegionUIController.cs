using UnityEngine;
using UnityEngine.UI;

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
    TMPro.TextMeshProUGUI regionNameLabel;

    [SerializeField]
    TMPro.TextMeshProUGUI occupationLabel;

    [SerializeField]
    BuildingButtonController[] buildingButtons;



    RegionController activeRegion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideCanvas();
        HideBuildMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCanvas(RegionController region)
    {
        /*
        // Weird hacky solution?
        if (!region.IsOnScreenLeft())
        {
            mainRectTransform.anchorMin = new Vector2(0, .95f);
            mainRectTransform.anchorMax = new Vector2(.25f, .95f);

            buildRectTransform.anchorMin = new Vector2(.25f, .95f);
            buildRectTransform.anchorMax = new Vector2(.45f, .95f);
        }
        else
        {
            mainRectTransform.anchorMin = new Vector2(.75f, .95f);
            mainRectTransform.anchorMax = new Vector2(1, .95f);

            buildRectTransform.anchorMin = new Vector2(.55f, .95f);
            buildRectTransform.anchorMax = new Vector2(.75f, .95f);
        }
        */

        mainCanvasGroup.alpha = 1f;
        mainCanvasGroup.interactable = true;
        mainCanvasGroup.blocksRaycasts = true;

        buildingTransform.gameObject.SetActive(!region.IsRegionOccupied());
        armyTransform.gameObject.SetActive(region.IsRegionOccupied());

        regionNameLabel.text = region.GetRegionName();
        occupationLabel.text = region.IsRegionOccupied() ? "OCCUPIED" : "LIBERATED";

        if (region.IsRegionOccupied())
        {
            HideBuildMenu();
        }

        for (int i = 0; i < buildingButtons.Length; i++)
        {
            if (region.GetBuildingSlots() > i)
            {
                buildingButtons[i].gameObject.SetActive(true);
                buildingButtons[i].SetBuilding(region.GetConstructedBuildings()[i]);
            }
        }

        activeRegion = region;
    }

    public void HideCanvas()
    {
        mainCanvasGroup.alpha = 0f;
        mainCanvasGroup.interactable = false;
        mainCanvasGroup.blocksRaycasts = false;

        activeRegion = null;

        HideBuildMenu();
    }

    public void ShowBuildMenu()
    {
        buildCanvasGroup.alpha = 1f;
        buildCanvasGroup.interactable = true;
        buildCanvasGroup.blocksRaycasts = true;
    }

    public void HideBuildMenu()
    {
        buildCanvasGroup.alpha = 0f;
        buildCanvasGroup.interactable = false;
        buildCanvasGroup.blocksRaycasts = false;

        for (int i = 0; i < buildingButtons.Length; i++)
        {
            buildingButtons[i].gameObject.SetActive(false);
        }
    }

}
