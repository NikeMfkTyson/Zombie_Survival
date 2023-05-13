using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool isGameOver;
    public int wave = 1;
    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI bulletsText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI playerHealthText;
    public GameObject waveTextCanvas;
    public float timeToDisappearItems = 10f;
    private ZombieController zombieControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        gameOverCanvas.SetActive(false);
        waveTextCanvas.SetActive(false);

        StartWave();        
    }

    void Update()
    {
        playerHealthText.text = "" + playerControllerScript.health / 10;
        waveText.text = "Wave " + wave;

        if(playerControllerScript.gunScript.isRecharging)
        {
            bulletsText.text = "Recharging";
        }
        else
        {
            bulletsText.text = "" + playerControllerScript.gunScript.currentAmountBullets;
        }                

        if(GameObject.FindGameObjectWithTag("Zombie") == null)
        {
            WaveComplete();
        }
    }

    public void StartWave()
    {
        StartCoroutine(AppearDisappear(waveTextCanvas));
        ZombiePool.SharedInstance.SpawnWave(wave);
        isGameActive = true;
    }

    private void WaveComplete()
    {
        wave++;
        StartWave();
    }

    public void GameOver()
    {
        isGameOver = true;
        isGameActive = false;
        gameOverCanvas.SetActive(true);
        spawnManagerScript.StopAllCoroutines();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator AppearDisappear(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(3);
        obj.SetActive(false);
    }
}
