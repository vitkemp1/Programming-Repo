using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject introBox;
    private Timer gameTimer;
    private float maxUpAngle=20;
    private float minDownAngle=-30;
    private float maxYAxisAngle=55;
    private float rotationSpeed = 30;
    private bool canShoot = true;
    private float delayInSeconds = 0.1f;    
    private float shootVolume =1;
    public AudioSource audioSource;
    public bool isGameOver = false;
    public bool isGameStarted = false;
    public int overalHits = 0;
    public int overalScore = 0;
    public int shipHealth = 100;       
    public Text scoreText;
    public Text hitsText;
    public Text shipHealthText;
    public GameObject barrelEnd;
    public GameObject bullets;
    public GameObject turret;
    public GameObject ShootingParticle;
    public AudioClip shootingSound;
    public AudioClip backgroundMusic;
    
    

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = GameObject.Find("Timer").GetComponent<Timer>();
        introBox = GameObject.Find("IntroBox");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            rotateTurret();
            shootTurret();
            displayScore();
        }
        
    }
    public void startGame()
    {
        audioSource.PlayOneShot(backgroundMusic);
        overalScore = 0;
        overalHits = 0;
        gameTimer.startTimer();
        displayScore();
        isGameStarted = true;
        introBox.SetActive(false);

    }
    public void stopGame()
    {
        isGameStarted = false;
        introBox.SetActive(true);
    }
    
    void displayScore()
    {
        scoreText.text = "Score : " + overalScore;
        hitsText.text = "Hits : " + overalHits;
        if (shipHealth < 0) shipHealth = 0;
        shipHealthText.text = "Ship Health : " + shipHealth;
    }
    void shootTurret()
    {
        if (Input.GetKey(KeyCode.Space)&&(canShoot))
        {
            audioSource.PlayOneShot(shootingSound);
            StartCoroutine(spawnBullets());
            canShoot = false;
            StartCoroutine(ShootDelay());
        }
        
    }
    IEnumerator spawnBullets()
    {
        yield return new WaitForSeconds(0f);
        Vector3 barrelPos = barrelEnd.transform.position;
        Instantiate(bullets, barrelPos, turret.transform.rotation);
        Instantiate(ShootingParticle, barrelPos, turret.transform.rotation);
    }
    void rotateTurret()
    {
        float rotationX = switchDegree(turret.transform.localEulerAngles.x);        
        float rotationY = switchDegree(turret.transform.localEulerAngles.y);
        
        if (Input.GetKey(KeyCode.UpArrow)&&(rotationX> minDownAngle))
        {
            rotateDirection("up");            
        }

        if (Input.GetKey(KeyCode.DownArrow)&&(rotationX< maxUpAngle))
        {
            rotateDirection("down");
        }

        if (Input.GetKey(KeyCode.LeftArrow)&&(rotationY > -1 * maxYAxisAngle))
        {
            rotateDirection("left");
        }

        if (Input.GetKey(KeyCode.RightArrow)&&(rotationY <  maxYAxisAngle))
        {
            rotateDirection("right");
        }
    }
    float switchDegree(float degree)
    {
        if (degree > 180)
        {
            degree -= 360;
        }
        return degree;
    }
    void rotateDirection(string direction)
    {        
        switch (direction)
        {   
            case "left":
                turret.transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed, Space.World);
                break;

            case "right":
                turret.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
                break;

            case "up":
                turret.transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed, Space.Self);
                break;

            case "down":
                turret.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed, Space.Self);
                break;
        }
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        canShoot = true;
    }
}
