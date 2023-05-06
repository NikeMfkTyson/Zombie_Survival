using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfPowerUp {none, shotSpeed, runSpeed, freeze}
public class PowerUp : MonoBehaviour
{
    public TypeOfPowerUp typeOfPowerUp;

    private void Start() 
    {
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
