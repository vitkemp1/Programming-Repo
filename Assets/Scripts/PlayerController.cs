using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;

public class PlayerController : MonoBehaviour
{
    private float maxUpAngle=20;
    private float minDownAngle=-30;
    private float maxYAxisAngle=55;
    private float rotationSpeed = 30;
    private bool canShoot = true;
    private float delayInSeconds = 0.05f;    
    public GameObject barrelEnd;
    public GameObject bullets;
    public GameObject turret;
    public GameObject ShootingParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateTurret();
        shootTurret();
    }

    void shootTurret()
    {
        if (Input.GetKey(KeyCode.Space)&&(canShoot))
        {            
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
