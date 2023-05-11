using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] powerUps;
    private float spawnPos = 25;
    public float timeToPowerUpSpawn = 10;
    void Start()
    {
        
        StartCoroutine(SpawnNewPowerUp());
    }

    
    void Update()
    {
        
    }

    private void SpawnPowerUp()
    {
        int index = Random.Range(0, powerUps.Length);
        float xPos = Random.Range(-spawnPos, spawnPos);
        float zPos = Random.Range(-spawnPos, spawnPos);
        Vector3 spawnPoint = new Vector3(xPos, 0, zPos);

        Instantiate(powerUps[index], spawnPoint, powerUps[index].transform.rotation);
    }

    public IEnumerator SpawnNewPowerUp()
    {
        yield return new WaitForSeconds(timeToPowerUpSpawn);
        SpawnPowerUp();
        StartCoroutine(SpawnNewPowerUp());
    }



}
