using UnityEngine;
using TMPro;

public class StatCanvasController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI countCounter;

    [SerializeField]
    TextMeshProUGUI turnCounter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turnCounter.text = "Turn " + GameManager.Instance.GetTurnCount();

        int occupiedCount = 0;

        for (int i = 0; i < RegionManager.Instance.GetAllRegions().Length; i++)
        {
            if (RegionManager.Instance.GetRegion(i).IsRegionOccupied())
            {
                occupiedCount += 1;
            }
        }

        countCounter.text = occupiedCount.ToString();
    }

    public void AdvanceTurn()
    {
        GameManager.Instance.AdvanceTurn();
    }
}
