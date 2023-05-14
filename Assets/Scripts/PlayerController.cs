using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private TypeOfPowerUp currentPowerUp = TypeOfPowerUp.none;
    public TypeOfGun currentGun = TypeOfGun.pistol;
    public bool hasFreezePowerUp = false;
    public int health;
    private float powerUpTime = 5;
    private Coroutine powerupCountdown;
    public Gun gunScript;
    private GameManager gameManagerScript;
    

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        gunScript = GetComponent<Pistol>();
        health = 1000;
        
    }

    void Update()
    {
        Move();
        gunScript.Shot();

        if(health <= 0)
        {
            health = 0;
            gameManagerScript.GameOver();
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(horizontalInput, 0, verticalInput, Space.World);
        
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); 
        // Нахождение катетов для расчёта тангенса, а в последствии и количества градусов угла. 
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; 
        // Нахождение тангенса угла и перевод его в градусы.
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
        // Вращение объекта на полученное количество градусов.
    }

    private void OnTriggerEnter(Collider other) 
    {        
        if(other.gameObject.CompareTag("PowerUp"))
        {
            if(gunScript.hasPowerUp)
            {                
                gunScript.hasPowerUp = false;
            }
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().typeOfPowerUp;
            SetBehaviour();
            StartCoroutine(PowerUpCountDownRoutine(powerUpTime));
            Destroy(other.gameObject);
            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            powerupCountdown = StartCoroutine(PowerUpCountDownRoutine(powerUpTime));
        }

        else
        {
            switch(other.gameObject.tag)
            {
                case "Pistol":
                currentGun = TypeOfGun.pistol;                
                gunScript = GetComponent<Pistol>();
                gameManagerScript.GunText("Pistol");
                break;
                case "Shotgun":
                currentGun = TypeOfGun.shotgun;
                gunScript = GetComponent<Shotgun>();
                gameManagerScript.GunText("Shotgun");
                break;
                case "Riffle":
                currentGun = TypeOfGun.rifle;
                gunScript = GetComponent<Riffle>();
                gameManagerScript.GunText("Riffle");
                break;
                case "Minigun":
                currentGun = TypeOfGun.minigun;
                gunScript = GetComponent<Minigun>();
                gameManagerScript.GunText("Minigun");
                break;
            }
            gunScript.NewGun();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator PowerUpCountDownRoutine(float delay)
    {        
        yield return new WaitForSeconds(delay);
        currentPowerUp = TypeOfPowerUp.none;
        hasFreezePowerUp = false;   
        gunScript.hasPowerUp = false; 
        SetBehaviour();    
    }

    private void SetBehaviour()
    {
        switch(currentPowerUp)
        {
            case TypeOfPowerUp.none: 
                speed = 5f;
                break;
            case TypeOfPowerUp.shotSpeed:
                speed = 5f;
                gunScript.hasPowerUp = true;
                break;
            case TypeOfPowerUp.runSpeed:
                speed = 7f;
                break;
            case TypeOfPowerUp.freeze:
                speed = 5f;
                hasFreezePowerUp = true;
                break;
        }        
    }

    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.CompareTag("Zombie"))
        {
            health -= other.gameObject.GetComponent<ZombieController>().strength;
        }
    }

    
}
