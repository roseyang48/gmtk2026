using UnityEngine;
using TMPro;

public class DefeatSceneController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI defeatCauseText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string cause)
    {
        defeatCauseText.text = cause;
    }

    public void ReturnToMainMenu()
    {
        SceneSwitcher.Instance.LoadScene(SceneSwitcher.SceneType.MAIN_MENU);
    }
}
