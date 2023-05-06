using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfZombie {zombie, zombieFast, zombieTank}
public class ZombieController : Zombie
{
    public TypeOfZombie typeOfZombie;
    public GameObject player;
    void Start()
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

    void Update()
    {
        Move(player);// MoveTowards
    }
}
