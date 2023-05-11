using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfGun {pistol, shotgun, minigun, rifle}
public class Gun : MonoBehaviour
{
    protected TypeOfGun typeOfGun;
    public int amountBullets;
    protected int currentAmountBullets;
    protected float shotDelay;
    protected float rechargeTime;
    public bool isActive;
    protected bool isRecharging = false;
    protected bool readyToShot = true;
    protected GameObject bulletPrefab;
  

    public virtual void Shot()
    {              
        if(readyToShot && currentAmountBullets > 0 && Input.GetKey(KeyCode.Mouse0))
        {
            print(currentAmountBullets);
            
            bulletPrefab = ObjectPool.SharedInstance.GetPooledObject();
            if(bulletPrefab != null)
            {
                // print("Shot");
                bulletPrefab.transform.position = transform.position;
                bulletPrefab.transform.rotation = transform.rotation;
                bulletPrefab.SetActive(true);
                currentAmountBullets--;
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
                // print("Recharge");
            }  
        }                               
    }

    protected IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(shotDelay);
        readyToShot = true;
    }

    protected IEnumerator Recharge()
    {
        yield return new WaitForSeconds(rechargeTime);
        currentAmountBullets = amountBullets;
        readyToShot = true;
        isRecharging = false;
    }
}
