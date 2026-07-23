using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    RegionUIController regionUIController;

    [SerializeField]
    RegionController[] regions;

    [SerializeField]
    Building[] buildingOptions;

    public static GameManager Instance;

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

    public void RegionSelected(int regionNumber)
    {
        regionUIController.ShowCanvas(regions[regionNumber]);
    }

    public void BuildingSelected(int buildingSlot)
    {
        regionUIController.ShowBuildMenu();
    }

    public Building[] GetBuildingOptions()
    {
        return buildingOptions;
    }

    public void CancelAction()
    {
        regionUIController.HideCanvas();
    }
}
