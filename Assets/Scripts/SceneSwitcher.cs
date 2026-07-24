using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneSwitcher : MonoBehaviour
{
    //Singleton Setup
    public static SceneSwitcher Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    //Rest of Class
    string mainMenuSceneName = "MainMenu";
    string mapSceneName = "MapScene";
    string combatSceneName = "CombatScene";


    bool awaitingSceneLoading = false;

    //TODO UNTIL WE FIGURE SOMETHING BETTER
    bool pauseOpen = false;
    bool cursorState = true;
    float currentTimeScale = 1;

    public enum SceneType
    {
        MAIN_MENU,
        MAP,
        MAP_POST,
        COMBAT,
    }

    public void LoadScene(SceneType sceneType, Army playerArmy, Army enemyArmy, bool wasSuccessful)
    {
        if (awaitingSceneLoading)
        {
            return;
        }

        //Always reset to timescale 1 when switching scenes
        Time.timeScale = 1f;
        awaitingSceneLoading = true;

        switch (sceneType)
        {
            case SceneType.MAIN_MENU:
                {
                    StartCoroutine(LoadMainMenu());
                    break;
                }

            case SceneType.MAP:
                {
                    StartCoroutine(LoadMap());
                    break;
                }

            case SceneType.MAP_POST:
                {
                    StartCoroutine(LoadMap(wasSuccessful));
                    break;
                }

            case SceneType.COMBAT:
                {
                    StartCoroutine(LoadCombat(playerArmy, enemyArmy));
                    break;
                }
                /*
                case SceneType.NAVIGATION:
                    {
                        StartCoroutine(LoadNavigation());
                        break;
                    }

                case SceneType.VICTORY:
                    {
                        StartCoroutine(LoadVictory());
                        break;
                    }

                case SceneType.DEFEAT:
                    {
                        StartCoroutine(LoadDefeat());
                        break;
                    }

                case SceneType.LOADOUT:
                    {
                        StartCoroutine(LoadLoadout());
                        break;
                    }
                case SceneType.UPGRADE_HISTORY:
                    {
                        StartCoroutine(LoadUpgradeHistory());
                        break;
                    }
                    */
        }
    }

    public void LoadScene(SceneType sceneType, Army playerArmy, Army enemyArmy)
    {
        LoadScene(sceneType, playerArmy, enemyArmy, false);
    }

    public void LoadScene(SceneType sceneType)
    {
        LoadScene(sceneType, null, null);
    }

    public void LoadScene(SceneType sceneType, bool wasSuccessful)
    {
        LoadScene(sceneType, null, null, wasSuccessful);
    }

    IEnumerator LoadMainMenu()
    {
        AsyncOperation mainMenuLoading = SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Single);

        yield return new WaitUntil(() => { return mainMenuLoading.isDone; });

        awaitingSceneLoading = false;
        Cursor.visible = true;
    }

    IEnumerator LoadMap()
    {
        AsyncOperation mapLoading = SceneManager.LoadSceneAsync(mapSceneName, LoadSceneMode.Single);

        yield return new WaitUntil(() => { return mapLoading.isDone; });

        GameManager.Instance.UpdateUIReferences();

        awaitingSceneLoading = false;
        Cursor.visible = true;
    }

    IEnumerator LoadMap(bool wasSuccessful)
    {
        AsyncOperation mapLoading = SceneManager.LoadSceneAsync(mapSceneName, LoadSceneMode.Single);

        yield return new WaitUntil(() => { return mapLoading.isDone; });

        GameManager.Instance.UpdateUIReferences();
        GameManager.Instance.ConcludeAssault(wasSuccessful);

        awaitingSceneLoading = false;
        Cursor.visible = true;
    }

    IEnumerator LoadCombat(Army playerArmy, Army enemyArmy)
    {
        AsyncOperation combatLoading = SceneManager.LoadSceneAsync(combatSceneName, LoadSceneMode.Single);

        yield return new WaitUntil(() => { return combatLoading.isDone; });

        CombatHandler.Instance.Initialize(playerArmy, enemyArmy);

        awaitingSceneLoading = false;
        Cursor.visible = true;
    }
}
