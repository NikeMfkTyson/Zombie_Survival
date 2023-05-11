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
    public TextMeshProUGUI waveText;
    public GameObject waveTextCanvas;
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
        waveText.text = "Wave " + wave;
        if(GameObject.FindGameObjectWithTag("Zombie") == null)
            WaveComplete();
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
        if(SceneManager.sceneCountInBuildSettings == 1)
            SceneManager.LoadScene(0);
        else
            Application.Quit();
    }

    private IEnumerator AppearDisappear(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(3);
        obj.SetActive(false);
    }
}
