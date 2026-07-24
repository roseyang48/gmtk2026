using UnityEngine;

public class MapResizer : MonoBehaviour
{
    [SerializeField] GameObject map;
    [SerializeField] Vector2 baseSize;
    void Start()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0,0));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;
        Vector2 newScale = new Vector2(width/baseSize.x, height/baseSize.y);
        map.transform.localScale = newScale;
    }
}
