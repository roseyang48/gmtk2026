using UnityEngine;

public class PopUpHandler : MonoBehaviour
{
    public static PopUpHandler Instance;
    [SerializeField] GameObject popUpObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnPopup(string text, Vector2 position)
    {
        GameObject popUp = Instantiate(popUpObj, position, Quaternion.identity);
    }
}
