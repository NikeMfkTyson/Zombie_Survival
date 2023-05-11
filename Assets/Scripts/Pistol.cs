using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    void Start()
    {
        shotDelay = 0.5f;
        amountBullets = 7;
        currentAmountBullets = 7;
        rechargeTime = 1.5f;
    }

    
    void Update()
    {
        // AttachToPlayer(isActive);
    }

    // private void OnCollisionEnter(Collision other) 
    // {
    //     if(other.gameObject.CompareTag("Player"))
    //     {
    //         Destroy(other.gameObject.GetComponent<PlayerController>().currentGun);
    //         isActive = true;
    //         other.gameObject.GetComponent<PlayerController>().currentGun = this.gameObject;
    //     }
    // }

    // private void AttachToPlayer(bool active)
    // {
    //     if(active)
    //     {
    //         // transform.position = player.transform.position;
    //         // transform.rotation = player.transform.rotation;
    //         gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    //     }        
    // }
}
