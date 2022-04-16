using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyerScript : MonoBehaviour
{
    //private float xPosition;
    //private float yPosition=0.02f;    
    private float sankingDepth = -11;
    private float speed=20f;
    private float maxXPosition = 200;
    private float xFromZPosition = 110;
    private float yPosition = -2.5f;
    private bool shipIsSinking=false;
    public int shipHealth = 100;
    public int hitCount=0;
    private int scorePerHit = 2;
    public GameObject smallExplosion;
    public GameObject bigExplosion;
    //public GameObject smokeParticle;
    //public GameObject destroyerShip;
    // Start is called before the first frame update
    void Start()
    {
        setPosition(new Vector3(-1*maxXPosition, -2.5f, 60.9f));        
     
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.x > maxXPosition)||(transform.position.y<sankingDepth))
        {
            shipIsSinking = false;
            transform.localRotation = Quaternion.Euler(0, 90, 0);
            setPosition(definePosition());
            shipHealth = 100;
            hitCount = 0;
            speed = 20;
        }
        MoveDestroyer();
        if (shipIsSinking)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * speed*3);
        }

    }
    
    Vector3 definePosition()
    {
        float zPosition = Random.Range(50, 150);        
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
            //Debug.Log("Health is : " + shipHealth);
            if (shipHealth <= 0)
             {
                 shipSink(point);
             }
             else
             {
                 Instantiate(smallExplosion, point, transform.rotation);
             }
            hitCount+=scorePerHit;      
        }
        
    }
    void shipSink(Vector3 point)
    {
        Instantiate(bigExplosion, point, transform.rotation);
        speed = 2;
        shipIsSinking = true;        
    }
}
