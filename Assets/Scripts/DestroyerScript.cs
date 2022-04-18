using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyerScript : MonoBehaviour
{    
    private float sankingSpeed = 2;
    private float normalSpeed = 20;
    private float speed = 20f;
    private float sankingDepth = -11;    
    private float maxXPosition = 200;
    private float xFromZPosition = 110;
    private float yPosition = -2.5f;
    private float zMinPosition = 50;
    private float zMaxPosition = 120;
    private bool shipIsSinking=false;
    private PlayerController playerControllerScript;    
    private int scorePerHit = 2;    
    public int shipHealth = 100;
    public int hitCount = 0;
    public GameObject smallExplosion;
    public GameObject bigExplosion;
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public AudioClip hitSound;
    
    //public GameObject destroyerShip;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isGameStarted)
        {
            if ((transform.position.x > maxXPosition) || (transform.position.y < sankingDepth))
            {
                shipIsSinking = false;
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                setPosition(definePosition());
                shipHealth = 100;
                playerControllerScript.shipHealth = shipHealth;
                hitCount = 0;
                speed = normalSpeed;
            }
            MoveDestroyer();
            if (shipIsSinking)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * speed * 3);
            }
        }
        

    }
    
    Vector3 definePosition()
    {
        float zPosition = Random.Range(zMinPosition, zMaxPosition);
        shipHealth = 100;
        return new Vector3(-1 * (xFromZPosition + zPosition), yPosition, zPosition);
    }
    void setPosition(Vector3 position)
    {
        transform.position = position;
    }
    void MoveDestroyer()
    {        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        Vector3 point = collision.gameObject.transform.position;
        if (collision.gameObject.name.Contains("Bullet"))
        {
            shipHealth -=scorePerHit;            
            if (shipHealth <= 0)
             {
                 shipSink(point);
             }
             else
             {
                 Instantiate(smallExplosion, point, transform.rotation);
                audioSource.PlayOneShot(hitSound);
             }
            hitCount+=scorePerHit;
            playerControllerScript.overalHits++;
            playerControllerScript.shipHealth = shipHealth;
        }
        
    }
    void shipSink(Vector3 point)
    {
        Instantiate(bigExplosion, point, transform.rotation);
        audioSource.PlayOneShot(explosionSound);
        speed = sankingSpeed;
        if (!shipIsSinking)
        {
            playerControllerScript.overalScore++;
            shipIsSinking = true;
        }
        
        
    }
    
}
