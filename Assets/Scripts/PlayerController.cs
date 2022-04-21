using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private string battleShipName = "Russian Battle Ship Health : ";
    private string destroyerName = "Russian Destroyer Health : ";    
    private GameObject introBox;
    private Timer gameTimer;
    private float maxUpAngle=20;
    private float minDownAngle=-30;
    private float maxYAxisAngle=55;
    private float rotationSpeed = 30;
    private bool canShoot = true;
    private float delayInSeconds = 0.1f;            
    public bool isGameStarted = false;
    public int overalHits = 0;
    private int m_overalScore=0;
    public int overalScore              // ENCAPSULATION
    {
        get { return m_overalScore; }
        set { m_overalScore = value < 0 ? 0 : value; }
    }
    public int destroyerHealth = 100;
    public int battleShipHealth = 100;
    public Text scoreText;
    public Text hitsText;
    public Text destroyerHealthText;
    public Text battleShipHealthText;
    public Text introText;
    public Text startButtonText;
    public Text exitButtonText;
    public GameObject barrelEnd;
    public GameObject bullets;
    public GameObject turret;
    public GameObject ShootingParticle;
    public AudioSource audioSource;
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
            escapeGame();
        }
        
    }
    public void quitGame()
    {
        Application.Quit();
    }
    void escapeGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stopGame();
        }

    }
    public void startGame()
    {
        audioSource.PlayOneShot(backgroundMusic);
        m_overalScore = 0;
        overalHits = 0;
        gameTimer.startTimer();
        displayScore();
        isGameStarted = true;
        introBox.SetActive(false);

    }
    public void stopGame()
    {
        isGameStarted = false;
        setIntro();
        introBox.SetActive(true);
    }
    void setIntro() 
    {
        if (m_overalScore > 0)
        {
            introText.text = "Good Job! You sank " + m_overalScore + " Russian War Ships! They will not bomb civilians any more! Are you willing to fight again?";
        }
        else
        {
            introText.text = "Russian War Ships bomb civilians! We need you to fight against them!";
        }
        startButtonText.text = "Fight!";
        exitButtonText.text = "Quit";
        
    }
    void displayScore()
    {
        scoreText.text = "Score : " + m_overalScore;
        hitsText.text = "Hits : " + overalHits;
        setShipHealthText(battleShipHealth, battleShipHealthText, battleShipName); 
        setShipHealthText(destroyerHealth, destroyerHealthText, destroyerName);
    }
    void setShipHealthText(int shipHealth, Text healthText, string shipName)
    {
        if (shipHealth < 0) shipHealth = 0;
        healthText.text = shipName + shipHealth;
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
