using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonController : MonoBehaviour
{
    [SerializeField]
    int buildingIndex = -1;

    [SerializeField]
    Image buildingIcon;

    [SerializeField]
    TMPro.TextMeshProUGUI buildingName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildingSelected()
    {
        GameManager.Instance.BuildingSelected(buildingIndex);
    }

    public void SetBuilding(Building building)
    {
        if (building == null)
        {
            buildingName.text = "Empty";
        }
        else
        {
            buildingName.text = building.GetBuildingName();
        }
    }
}
