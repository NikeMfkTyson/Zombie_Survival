using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] powerUps;
    public GameObject[] guns;
    private float positionLimitX = 15;
    private float positionLimitZ = 25;
    public float timeToPowerUpSpawn = 10;
    public float timeToGunSpawn = 33;
    private Vector3 spawnPos;
    private int index;

    void Start()
    {
        StartCoroutine(SpawnGun());
        StartCoroutine(SpawnNewPowerUp());
    }

    

    private void GenerateSpawnPos(GameObject[] mass)
    {
        index = Random.Range(0, mass.Length);
        float xPos = Random.Range(-positionLimitX, positionLimitX);
        float zPos = Random.Range(-positionLimitZ, positionLimitZ);
        spawnPos = new Vector3(xPos, 0, zPos);

        
    }

    private IEnumerator SpawnNewPowerUp()
    {
        yield return new WaitForSeconds(timeToPowerUpSpawn);
        GenerateSpawnPos(powerUps);
        Instantiate(powerUps[index], spawnPos, powerUps[index].transform.rotation);
        StartCoroutine(SpawnNewPowerUp());
    }

    private IEnumerator SpawnGun()
    {
        yield return new WaitForSeconds(timeToGunSpawn);
        GenerateSpawnPos(guns);
        Instantiate(guns[index], spawnPos, guns[index].transform.rotation);
        StartCoroutine(SpawnGun());
    }

}
