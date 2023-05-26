using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = false;
    public bool isGameOver = false;
    // private bool isPaused = false;
    public int score;
    public int bestScore;
    public int wave = 1;
    public float timeToDisappearItems = 10f; 
    public TextMeshProUGUI bulletsText;
    public TextMeshProUGUI gameOverOrPauseText;
    public TextMeshProUGUI yourScoreText;
    public TextMeshProUGUI gunText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI scoreText;
    public GameObject waveTextCanvas;
    public GameObject resumeButton;
    public GameObject gameOverCanvas;
    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;    
    private ZombieController zombieControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        gameOverCanvas.SetActive(false);
        waveTextCanvas.SetActive(false);
        resumeButton.SetActive(false);

        gunText.text = "Pistol";

        StartWave();        
    }

    void Update()
    {
        playerHealthText.text = "" + playerControllerScript.health / 10;
        waveText.text = "Wave " + wave;
        scoreText.text = "" + score;

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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void AddScore(int i)
    {
        score += i;
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
        gameOverOrPauseText.text = "G A M E   O V E R";

        if(score > MainManager.Instance.highScore)
        {
            yourScoreText.text = "New Record!";
            MainManager.Instance.highScore = score;
        }
        
        isGameOver = true;
        isGameActive = false;
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
        resumeButton.SetActive(false);
        spawnManagerScript.StopAllCoroutines();
    }

    public void Pause()
    {
        gameOverOrPauseText.text = "P A U S E";
        yourScoreText.text = "Best score: " + MainManager.Instance.playerName + "  " + MainManager.Instance.highScore;
        isGameActive = false;
        // isPaused = true;        
        gameOverCanvas.SetActive(true);
        resumeButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isGameActive = true;
        // isPaused = false;        
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void GunText(string typeOfGun)
    {
        gunText.text = typeOfGun;
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
