using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destroyDistance = 25;
    private float speed = 15;
    private Transform playerTransform;
    

    private void Start() 
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    
    void Update()
    {        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        float distanceX = transform.position.x - playerTransform.position.x;
        float distanceZ = transform.position.z - playerTransform.position.z;

        if(distanceX > destroyDistance || distanceX < -destroyDistance) 
            gameObject.SetActive(false);
        if(distanceZ > destroyDistance || distanceZ < -destroyDistance)
            gameObject.SetActive(false);
    }
}
