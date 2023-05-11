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
        bulletPrefab = ObjectPool.SharedInstance.GetPooledObject();
        float angle = -5f;
        Quaternion direction = Quaternion.Euler(0, angle, 0) * transform.rotation;
        for(int i = 0; i < 11; i++)
        {    
            if(bulletPrefab != null)
            { 
                bulletPrefab.transform.position = transform.position;
                bulletPrefab.transform.rotation = direction;
                direction = Quaternion.Euler(0f, angle + 5f, 0f) * bulletPrefab.transform.rotation;
                print(direction.eulerAngles + "   " + transform.rotation.eulerAngles);
                bulletPrefab.SetActive(true);
            }

            angle += 5f;
        }
    }
}
