using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    public static ZombiePool SharedInstance;
    private List<GameObject> easyEnemies = new List<GameObject>();
    private List<GameObject> mediumEnemies = new List<GameObject>();
    private List<GameObject> hardEnemies = new List<GameObject>();
    private List<float> position;
    public GameObject[] objectToPool;
    int easyEnemy = 250;        
    int hardEnemy = 100;
    int mediumEnemy = 150;

    private void Awake() 
    {
        SharedInstance = this;
        CreateEnemies();
        GeneratePosition();
    }

    private void Start() 
    {
        
        // SpawnWave(3);

    }

    private void CreateEnemies()
    {
        GameObject e;        
        for(int i = 0; i < easyEnemy; i++)
        {
            e = Instantiate(objectToPool[0]);
            e.SetActive(false);
            easyEnemies.Add(e);
        }
        
        GameObject m;
        for(int i = 0; i < mediumEnemy; i++)
        {
            m = Instantiate(objectToPool[1]);
            m.SetActive(false);
            mediumEnemies.Add(m);
        }
        
        GameObject h;
        for(int i = 0; i < hardEnemy; i++)
        {
            h = Instantiate(objectToPool[2]);
            h.SetActive(false);
            hardEnemies.Add(h);
        }
    }

    public void SpawnWave(int wave)
    {
        int easy = CheckRange(wave * 5, easyEnemy);
        int medium = CheckRange(wave * 3, mediumEnemy);
        int hard = CheckRange(wave * 2, hardEnemy);

        Spawn(easy, easyEnemies);
        Spawn(medium, mediumEnemies);
        Spawn(hard, hardEnemies);
    }

    private void Spawn(int amount, List<GameObject> list)
    {
        for(int i = 0; i < amount; i++)
        {
            list[i].transform.position = SetPosition();
            list[i].SetActive(true);
        }
    }

    private int CheckRange(int i, int amount)
    {
        if(i > amount)
            return amount;
        else
            return i;
    }

    private Vector3 SetPosition()
    {
        float xPos = RandomNumber();
        float zPos = RandomNumber();        
        Vector3 randomPos = new Vector3(xPos, 0, zPos);
        return randomPos;
    }

    private float RandomNumber()
    {
        int i = Random.Range(0, position.Count);
        return position[i];
    }

    private void GeneratePosition()
    {
        position = new List<float>();
        for(float i = -70; i < -40; i++)
        {
            position.Add(i);
        }
        for(float i = 40; i < 70; i++)
        {
            position.Add(i);
        }
    }
}
