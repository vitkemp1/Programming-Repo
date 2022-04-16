using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRb;
    private float speed=4500;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        shootBullet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void shootBullet()
    {
        bulletRb.AddForce(transform.up * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.transform.parent.gameObject)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        
       // Debug.Log("Collide!");
    }
    
}
