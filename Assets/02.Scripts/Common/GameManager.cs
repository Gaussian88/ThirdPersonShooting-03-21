using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager g_Instance;
    public bool isGameOver = false;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private List<Transform> spawnPointList;
    private int maxCount = 10;
    void Awake()
    {
        if (g_Instance == null)
            g_Instance = this;
        else if(g_Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        //enemy = Resources.Load<GameObject>("Enemies/Enemy");
        //swat = Resources.Load<GameObject>("Enemies/swat");
        enemies = Resources.LoadAll<GameObject>("Enemies");
        CreateEnemies();
        var spawnpoint = GameObject.Find("SpawnPoints");
        if (spawnpoint != null)
            spawnpoint.GetComponentsInChildren<Transform>(spawnPointList);
        spawnPointList.RemoveAt(0);
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    void CreateEnemies()
    {
        GameObject enemyObjGroup = new GameObject("enemyObjGroup");
        for(int i =0; i < maxCount; i++)
        {
            int idx = Random.Range(0, enemies.Length);
            var enemy = Instantiate(enemies[idx].gameObject,enemyObjGroup.transform);
            enemy.name ="enemy: "+(i+1).ToString()+ "¸¶¸®";
            enemy.gameObject.SetActive(false);
            enemyList.Add(enemy);
           
        }

    }
    IEnumerator SpawnEnemies()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(3.0f);

            foreach (var _enemy in enemyList)
            {
                if(_enemy.activeSelf==false)
                {
                    int idx = Random.Range(0, spawnPointList.Count);
                    _enemy.transform.position = spawnPointList[idx].position;
                    _enemy.transform.rotation = spawnPointList[idx].rotation;
                    _enemy.gameObject.SetActive(true);
                    break;
                }
               
            }
           
        }

    }
}
