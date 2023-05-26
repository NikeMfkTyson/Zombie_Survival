using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfZombie {zombie, zombieFast, zombieTank}
public class ZombieController : Zombie
{
    public TypeOfZombie typeOfZombie;
    private GameObject player;
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;
    void Start()
    {        
        player = GameObject.Find("Player");
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        SetBehaviour();          
    }

    void Update()
    {

        if(!playerControllerScript.hasFreezePowerUp)
        {
            Move(player);// MoveTowards
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            GetDamage(other.gameObject.GetComponent<Bullet>().damage);
            gameManagerScript.AddScore(other.gameObject.GetComponent<Bullet>().damage);
        }
    }

    private void SetBehaviour()
    {
        if(typeOfZombie == TypeOfZombie.zombie)
        {
            health = 100;
            speed = 3;
            strength = 10;
        }            
        else if(typeOfZombie == TypeOfZombie.zombieFast)
        {
            health = 60;
            speed = 4;
            strength = 5;
        }            
        else
        {
            health = 140;
            speed = 2;
            strength = 15;
        }  
    }
}
