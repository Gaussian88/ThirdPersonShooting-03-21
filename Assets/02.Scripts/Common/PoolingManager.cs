using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static  PoolingManager P_Instance = null;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private List<GameObject> playerBulletList;
    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] private List<GameObject> EnemyBulletList;
    private int P_bulletCount = 10;
    private int E_bulletCount = 20;
    void Awake()
    {
        if (P_Instance == null)
            P_Instance = this;
        else if(P_Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        playerBullet = Resources.Load<GameObject>("Weapons/Bullet");
        EnemyBullet = Resources.Load<GameObject>("Weapons/E_Bullet");
        CreatePlayerBulletPool();
        CreateEnemyBulletPool();
    }
    void CreatePlayerBulletPool()
    {
        GameObject playerBulletObjGroup = new GameObject("playerBulletObjGroup");
        for(int i =0; i<P_bulletCount; i++)
        {
            GameObject _bullet =Instantiate(playerBullet,playerBulletObjGroup.transform);
            _bullet.name = $"bullet : {(i+1).ToString()} ¹ß ";
            _bullet.SetActive(false);
            playerBulletList.Add(_bullet);
        }
    }
    public GameObject GetPlayerBullet()
    {
        foreach(GameObject _bullet in playerBulletList)
        {
            if(_bullet.activeSelf ==false)
            {
                return _bullet;
            }
        }
        return null;
    }
    void CreateEnemyBulletPool()
    {
        GameObject EnemyBulletObjGroup = new GameObject("EnemyBulletObjGroup");
        for (int i = 0; i < E_bulletCount; i++)
        {
            GameObject _bullet = Instantiate(EnemyBullet, EnemyBulletObjGroup.transform);
            _bullet.name = $"bullet : {(i + 1).ToString()} ¹ß ";
            _bullet.SetActive(false);
            EnemyBulletList.Add(_bullet);
        }
    }
    public GameObject GetEnemyBullet()
    {
        foreach (GameObject _bullet in EnemyBulletList)
        {
            if (_bullet.activeSelf == false)
            {
                return _bullet;
            }
        }
        return null;
    }
}
