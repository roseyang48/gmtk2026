using UnityEngine;

public class RegionController : MonoBehaviour
{
    [SerializeField]
    int regionNumber = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsOnScreenLeft()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        return screenPos.x < Camera.main.scaledPixelWidth / 2;
    }

    public int GetRegionNumber()
    {
        return regionNumber;
    }
}
