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
    public InputField input;
    public Text playerName;
    
    void Start()
    {
        buttons.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void SavePlayerName()
    {        
        MainManager.Instance.playerName = input.text;
        playerName.text = "Player: " + MainManager.Instance.playerName;
        buttons.SetActive(true);
        titleScreen.SetActive(false);
        print(MainManager.Instance.playerName);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {

    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
