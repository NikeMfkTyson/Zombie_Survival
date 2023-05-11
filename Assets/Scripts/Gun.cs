using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfGun {pistol, shotgun, minigun, rifle}
public class Gun : MonoBehaviour
{
    protected TypeOfGun typeOfGun;
    public int amountBullets;
    public int currentAmountBullets { get; protected set; }
    public float shotDelay;
    protected float rechargeTime;
    public bool isRecharging = false; 
    protected bool readyToShot = true;
    protected GameObject bulletPrefab;
    public bool hasPowerUp = false;
  

    public virtual void Shot()
    {              
        if(readyToShot && currentAmountBullets > 0 && Input.GetKey(KeyCode.Mouse0))
        {
            bulletPrefab = ObjectPool.SharedInstance.GetPooledObject();
            if(bulletPrefab != null)
            {
                bulletPrefab.transform.position = transform.position;
                bulletPrefab.transform.rotation = transform.rotation;
                bulletPrefab.SetActive(true);
                if(!hasPowerUp)
                {
                    currentAmountBullets--;
                }                
            }
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

    protected IEnumerator ShotDelay()
    {
        float delay = shotDelay;
        if(hasPowerUp)
        {
            delay /= 2;
        }
        yield return new WaitForSeconds(delay);
        readyToShot = true;
    }

    protected IEnumerator Recharge()
    {
        yield return new WaitForSeconds(rechargeTime);
        currentAmountBullets = amountBullets;
        readyToShot = true;
        isRecharging = false;
    }

    public void NewGun()
    {
        StopAllCoroutines();
        currentAmountBullets = amountBullets;
        readyToShot = true;
        isRecharging = false;
    }
}
