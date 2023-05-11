using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    bool x = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(x)
        {
            RotateCube();
        }
    }

    private void RotateCube()
    {
        for(int i = 0; i < 10; i++)
        {
            transform.rotation *= Quaternion.Euler(0, -15, 0);
        }
        x = false;
    }
}
