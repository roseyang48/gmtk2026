using UnityEngine;

public class BuildOptionController : MonoBehaviour
{
    [SerializeField]
    Building buildingOption;

    [SerializeField]
    TMPro.TextMeshProUGUI buildingName;

    [SerializeField]
    TMPro.TextMeshProUGUI buildingGoldCost;

    [SerializeField]
    TMPro.TextMeshProUGUI buildingWoodCost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildingName.text = buildingOption.GetBuildingName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OptionSelected()
    {
        GameManager.Instance.BuildingOptionSelected(buildingOption);
    }
}
