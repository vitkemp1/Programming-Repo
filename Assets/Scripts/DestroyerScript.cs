using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyerScript : MonoBehaviour
{
    //private float xPosition;
    //private float yPosition=0.02f;    
    private float speed=20f;
    private float maxXPosition = 300;
    private float xFromZPosition = 150;
    private float yPosition = -2.5f;
    //public GameObject destroyerShip;
    // Start is called before the first frame update
    void Start()
    {
        setPosition(new Vector3(-300, -2.5f, 60.9f));        
     
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > maxXPosition)
        {
            setPosition(definePosition());
        }
        MoveDestroyer();
    }
    Vector3 definePosition()
    {
        float zPosition = Random.Range(60, 160);
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
    
}
