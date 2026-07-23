using UnityEngine;
using System.Collections.Generic;

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
            DontDestroyOnLoad(gameObject);
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

    public Building[] GetConstructedBuildings()
    {
        List<Building> buildings = new List<Building>();

        for (int i = 0; i < regions.Length; i++)
        {
            buildings.AddRange(regions[i].GetConstructedBuildings());
        }

        return buildings.ToArray();
    }
}
