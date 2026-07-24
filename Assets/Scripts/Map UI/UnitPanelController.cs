using UnityEngine;

public class UnitPanelController : MonoBehaviour
{
    [SerializeField]
    ArmyManager.UnitType unitType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildUnit()
    {
        GameManager.Instance.BuildUnit(unitType);
    }
}
