using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearGun : MonoBehaviour
{
    private GameManager gameManagerScript;
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Dissapear());
    }

    private IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(gameManagerScript.timeToDisappearItems);
        Destroy(gameObject);
    }
}
