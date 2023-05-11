using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    void Start()
    {
        shotDelay = 1.5f;
        amountBullets = 8;
        rechargeTime = 4f;
    }

    public override void Shot()
    {
        if(readyToShot && currentAmountBullets > 0 && Input.GetKey(KeyCode.Mouse0))
        {
            ShotgunBullet();
            currentAmountBullets--;
            readyToShot = false;
            StartCoroutine(ShotDelay());
        }
        else if(!isRecharging)
        {
            if(Input.GetKeyDown(KeyCode.R) || currentAmountBullets == 0)
            {
                readyToShot = false;
                isRecharging = true;
                StartCoroutine(Recharge());
            }
        }
    }

    private void ShotgunBullet()
    {
        for(int i = 0; i < 11; i++)
        {    
            bulletPrefab = ObjectPool.SharedInstance.GetPooledObject();
            if(bulletPrefab != null)
            { 
                bulletPrefab.transform.position = transform.position;
                bulletPrefab.transform.rotation = Quaternion.Euler(0f, -5f + i, 0f) * transform.rotation;
                bulletPrefab.SetActive(true);
            }
        }
    }

    
}
