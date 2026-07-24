using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public static CombatHandler Instance;
    public enum Team {Player, Enemy}
    private Army survivors = new Army();
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject peasantObject;
    [SerializeField] private GameObject archerObject;
    [SerializeField] private GameObject infantryObject;
    [SerializeField] private GameObject cavalryObject;
    [SerializeField] private float playerSpawnPointY;
    [SerializeField] private float enemySpawnPointY;
    [SerializeField] private Color playerHatColor;
    [SerializeField] private Color enemyHatColor;
    private bool combatEnded = false;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text peasantCountText;
    [SerializeField] private TMP_Text rangedCountText;
    [SerializeField] private TMP_Text infantryCountText;
    [SerializeField] private TMP_Text cavalryCountText;
    [SerializeField] private Animator endMenuAnimator;
    [SerializeField] private float vanishTime;
    
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
    public void Initialize(Army playerArmy, Army enemyArmy)
    {
        StartCoroutine("SpawnPlayerArmy", playerArmy);
        StartCoroutine("SpawnEnemyArmy", enemyArmy);
    }
    public void AddSurvivor(ArmyManager.UnitType type)
    {
        switch(type)
        {
            case ArmyManager.UnitType.PEASANT:
                survivors.peasantCount++;
                break;
            case ArmyManager.UnitType.INFANTRY:
                survivors.infantryCount++;
                break;
            case ArmyManager.UnitType.RANGED:
                survivors.rangedCount++;
                break;
            case ArmyManager.UnitType.CAVALRY:
                survivors.cavalryCount++;
                break;
            default:
                Debug.LogError("UNRECOGNIZED UNIT TYPE ADDED AS SURVIVOR");
                break;
        }
    }
    private IEnumerator SpawnPlayerArmy(Army playerArmy)
    {
        float time = spawnTime / playerArmy.GetArmySize();
        for(int i = 0; i < playerArmy.peasantCount; i++)
        {
            GameObject obj = Instantiate(peasantObject, new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(
                    new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                    playerSpawnPointY), Quaternion.identity);
            obj.GetComponent<Unit>().Initialize(playerHatColor, Team.Player);
            obj.tag = "PlayerUnit";
            yield return new WaitForSeconds(time);
        }
        for(int i = 0; i < playerArmy.infantryCount; i++)
        {
            GameObject obj = Instantiate(infantryObject, new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(
                    new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                    playerSpawnPointY), Quaternion.identity);
            obj.GetComponent<Unit>().Initialize(playerHatColor, Team.Player);
            obj.tag = "PlayerUnit";
            yield return new WaitForSeconds(time);
        }
        for(int i = 0; i < playerArmy.cavalryCount; i++)
        {
            GameObject obj = Instantiate(cavalryObject, new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(
                    new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                    playerSpawnPointY), Quaternion.identity);
            obj.GetComponent<Unit>().Initialize(playerHatColor, Team.Player);
            obj.tag = "PlayerUnit";
            yield return new WaitForSeconds(time);
        }
        for(int i = 0; i < playerArmy.rangedCount; i++)
        {
            GameObject obj = Instantiate(archerObject, new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(
                    new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                    playerSpawnPointY), Quaternion.identity);
            obj.GetComponent<Unit>().Initialize(playerHatColor, Team.Player);
            obj.tag = "PlayerUnit";
            yield return new WaitForSeconds(time);
        }
    }
    private IEnumerator SpawnEnemyArmy(Army enemyArmy)
    {
        float time = spawnTime / enemyArmy.GetArmySize();
        for(int i = 0; i < enemyArmy.peasantCount; i++)
        {
            GameObject obj = Instantiate(peasantObject, new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(
                    new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                    enemySpawnPointY), Quaternion.Euler(0, 0, 180));
            obj.GetComponent<Unit>().Initialize(enemyHatColor, Team.Enemy);
            obj.tag = "EnemyUnit";
            yield return new WaitForSeconds(time);
        }
        for(int i = 0; i < enemyArmy.infantryCount; i++)
        {
            GameObject obj = Instantiate(infantryObject, new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(
                    new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                    enemySpawnPointY), Quaternion.Euler(0, 0, 180));
            obj.GetComponent<Unit>().Initialize(enemyHatColor, Team.Enemy);
            obj.tag = "EnemyUnit";
            yield return new WaitForSeconds(time);
        }
        for(int i = 0; i < enemyArmy.cavalryCount; i++)
        {
            GameObject obj = Instantiate(cavalryObject, new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(
                    new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                    enemySpawnPointY), Quaternion.Euler(0, 0, 180));
            obj.GetComponent<Unit>().Initialize(enemyHatColor, Team.Enemy);
            obj.tag = "EnemyUnit";
            yield return new WaitForSeconds(time);
        }
        for(int i = 0; i < enemyArmy.rangedCount; i++)
        {
            GameObject obj = Instantiate(archerObject, new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(
                    new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                    enemySpawnPointY), Quaternion.Euler(0, 0, 180));
            obj.GetComponent<Unit>().Initialize(enemyHatColor, Team.Enemy);
            obj.tag = "EnemyUnit";
            yield return new WaitForSeconds(time);
        }
    }

    public void EndCombat(Team winner, ArmyManager.UnitType type)
    {
        if(!combatEnded)
        {
            combatEnded = true;
            switch(winner)
            {
                case Team.Player:
                    titleText.text = "Count Conquered!";
                    StartCoroutine("RemoveUnits", "PlayerUnit");
                    break;
                case Team.Enemy:
                    titleText.text = "Down For The Count...";
                    StartCoroutine("RemoveUnits", "EnemyUnit");
                    break;
                default:
                    Debug.LogError("UNRECOGNIZED WINNER");
                    break;
            }
            endMenuAnimator.SetTrigger("OpenMenu");

        }
        if(winner == Team.Player)
        {
            AddSurvivor(type);
        }
        peasantCountText.text = survivors.peasantCount.ToString();
        infantryCountText.text = survivors.infantryCount.ToString();
        rangedCountText.text = survivors.rangedCount.ToString();
        cavalryCountText.text = survivors.cavalryCount.ToString();
    }
    private IEnumerator RemoveUnits(string team)
    {
        yield return new WaitForEndOfFrame();
        GameObject[] objects = GameObject.FindGameObjectsWithTag(team);
        foreach(GameObject obj in objects)
        {
            Unit unit = obj.GetComponent<Unit>();
            unit.Die();
            yield return new WaitForSeconds(vanishTime/objects.Length);
        }
    }
}
