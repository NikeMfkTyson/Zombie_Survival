using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfPowerUp {none, shotSpeed, runSpeed, freeze}
public class PowerUp : MonoBehaviour
{
    public TypeOfPowerUp typeOfPowerUp;
    private GameManager gameManagerScript;

    private void Start() 
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(gameManagerScript.timeToDisappearItems);
        Destroy(gameObject);
    }

    
}
