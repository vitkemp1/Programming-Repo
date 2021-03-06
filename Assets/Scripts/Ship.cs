using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    protected float sinkingSpeed = 2;
    protected float normalSpeed = 20;
    protected float speed = 20f;
    protected float sankingDepth = -11;
    protected float maxXPosition = 200;
    protected float xFromZPosition = 110;
    protected float yPosition = -2.5f;
    protected float zMinPosition = 50;
    protected float zMaxPosition = 120;
    protected bool shipIsSinking = false;
    protected PlayerController playerControllerScript;
    protected int scorePerHit = 2;
    public int shipHealth = 100;
    public int hitCount = 0;
    public GameObject smallExplosion;
    public GameObject bigExplosion;
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public AudioClip hitSound;
   
    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        setValues();
    }

    void Update()
    {
        sail(); //ABSTRACTION

    }
    void sail()
    {
        if (playerControllerScript.isGameStarted)
        {
            if ((transform.position.x > maxXPosition) || (transform.position.y < sankingDepth))
            {
                shipIsSinking = false;
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                setPosition(definePosition());
                shipHealth = 100;
                setShipHealth();
                hitCount = 0;
                speed = normalSpeed;
                changeSpeed();
            }
            MoveDestroyer();
            if (shipIsSinking)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * speed * 3);
            }
        }
    }
    public virtual void changeSpeed()
    {

    }
    protected abstract void setShipHealth();
    protected abstract void setValues();
    
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
            shipHealth -= scorePerHit;
            if (shipHealth <= 0)
            {
                shipSink(point);
            }
            else
            {
                Instantiate(smallExplosion, point, transform.rotation);
                audioSource.PlayOneShot(hitSound);
            }
            hitCount += scorePerHit;
            playerControllerScript.overalHits++;
            setShipHealth();
        }

    }
    
    void shipSink(Vector3 point)
    {
        Instantiate(bigExplosion, point, transform.rotation);
        audioSource.PlayOneShot(explosionSound);
        speed = sinkingSpeed;
        if (!shipIsSinking)
        {
            playerControllerScript.overalScore++;
            shipIsSinking = true;
        }


    }
}
