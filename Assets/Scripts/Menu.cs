using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject buttons;
    public GameObject controlsCanvas;
    public InputField input;
    public Text playerName;
    
    void Start()
    {
        
        controlsCanvas.SetActive(false);

        if(MainManager.Instance.playerName != "")
        {
            playerName.text = "Player: " + MainManager.Instance.playerName;
            buttons.SetActive(true);
            titleScreen.SetActive(false);
        }
        else
        {
            NewPlayer();
        }
    }

    public void SavePlayerName()
    {        
        MainManager.Instance.playerName = input.text;
        playerName.text = "Player: " + MainManager.Instance.playerName;
        buttons.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Back()
    {
        controlsCanvas.SetActive(false);
        buttons.SetActive(true);
    }

    public void Controls()
    {
        controlsCanvas.SetActive(true);
        buttons.SetActive(false);
    }

    public void NewPlayer()
    {
        buttons.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void Exit()
    {
        MainManager.Instance.SaveData();
        
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else            
            Application.Quit();
        #endif
    }
}
