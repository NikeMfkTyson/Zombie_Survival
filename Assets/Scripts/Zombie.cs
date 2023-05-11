using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float m_speed;
    public float speed 
    {
        get {return m_speed;} 
        set 
        { 
            if(value >= 0 && value <= 5)
                m_speed = value;
            else
                Debug.LogError("Speed can be in the range from 0 to 5");
        } 
    }
    private int m_strangth;
    public int strength
    {
        get {return m_strangth;}
        set 
        {
            if(value > 0 && value <= 100)
                m_strangth = value;
            else 
                Debug.LogError("Strangth can be in the range from 1 to 100");
        }
    }
    private int m_health;
    public int health
    {
        get {return m_health;}
        set 
        {
            if(value >= 1 && value <= 400)
                m_health = value;
            else
                Debug.LogError("Health can be in the range from 1 to 400");
        }
    }
   

    public void GetDamage(int damage)
    {
        m_health -= damage;
        if(m_health <= 0)
        {
            gameObject.SetActive(false);
            m_health = health;
        }
    }

    protected virtual void Move(GameObject target)
    {
        float maxDistanceDelta = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, maxDistanceDelta);
    }
}
