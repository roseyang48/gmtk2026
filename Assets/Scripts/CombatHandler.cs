using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public static CombatHandler Instance;
    public enum Team {Player, Enemy}
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject peasantObject;
    [SerializeField] private GameObject archerObject;
    [SerializeField] private GameObject infantryObject;
    [SerializeField] private GameObject cavalryObject;
    [SerializeField] private float playerSpawnPointY;
    [SerializeField] private float enemySpawnPointY;
    [SerializeField] private Color playerHatColor;
    [SerializeField] private Color enemyHatColor;
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
}
