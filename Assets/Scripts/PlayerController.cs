using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private GameObject bulletPrefab;
    private Rigidbody playerRb;
    private bool readyToShot = true;
    private float _shotDelay = 0.5f;
    private TypeOfPowerUp currentPowerUp = TypeOfPowerUp.none;
    [SerializeField] private bool hasPowerup = false;
    private float powerUpTime = 5;
    private Coroutine powerupCountdown;
    

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        Move();
        Shot();
        SetBehaviour();
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

    private void Shot()
    {        
        if(Input.GetKey(KeyCode.Mouse0) && readyToShot)
        {
            bulletPrefab = ObjectPool.SharedInstance.GetPooledObject();
            if(bulletPrefab != null)
            {
                bulletPrefab.transform.position = transform.position;
                bulletPrefab.transform.rotation = transform.rotation;
                bulletPrefab.SetActive(true);
            }
            readyToShot = false;
            StartCoroutine(ShotDelay());
        }
    }

    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(_shotDelay);
        readyToShot = true;
    }

    private void OnTriggerEnter(Collider other) 
    {        
        if(other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().typeOfPowerUp;
            StartCoroutine(PowerUpCountDownRoutine(powerUpTime));
            Destroy(other.gameObject);
            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            powerupCountdown = StartCoroutine(PowerUpCountDownRoutine(powerUpTime));
        }
    }

    private IEnumerator PowerUpCountDownRoutine(float delay)
    {        
        yield return new WaitForSeconds(delay);
        currentPowerUp = TypeOfPowerUp.none;
        hasPowerup = false;
        
    }

    private void SetBehaviour()
    {
        switch(currentPowerUp)
        {
            case TypeOfPowerUp.none: 
                speed = 5f;
                _shotDelay = 0.5f;
                break;
            case TypeOfPowerUp.shotSpeed:
                speed = 5f;
                _shotDelay = 0.2f;
                break;
            case TypeOfPowerUp.runSpeed:
                speed = 7f;
                _shotDelay = 0.5f;
                break;
        }        
    }

    
}
