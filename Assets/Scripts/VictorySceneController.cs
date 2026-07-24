using UnityEngine;
using TMPro;

public class VictorySceneController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI victoryTurnsTopText;

    [SerializeField]
    TextMeshProUGUI victoryTurnsCount;

    [SerializeField]
    TextMeshProUGUI victoryTurnsBottomText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(int turnCount)
    {
        victoryTurnsCount.text = turnCount.ToString();
        victoryTurnsBottomText.fontSize = victoryTurnsTopText.fontSize;
    }

    public void ReturnToMainMenu()
    {
        SceneSwitcher.Instance.LoadScene(SceneSwitcher.SceneType.MAIN_MENU);
    }
}
