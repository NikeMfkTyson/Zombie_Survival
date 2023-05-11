using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Gun
{
    void Start()
    {
        shotDelay = 0.1f;
        amountBullets = 100;
        rechargeTime = 5f;
    }

}
