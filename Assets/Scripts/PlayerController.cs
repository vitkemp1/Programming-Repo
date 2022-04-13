using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;

public class PlayerController : MonoBehaviour
{
    private float maxUpAngle=20;
    private float minDownAngle=-50;
    private float maxYAxisAngle=60;
    private float rotationSpeed = 50;
    public GameObject turret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateTurret();

    }
    void rotateTurret()
    {
        float rotationX = switchDegree(turret.transform.localEulerAngles.x);        
        float rotationY = switchDegree(turret.transform.localEulerAngles.y);
        
        if (Input.GetKey(KeyCode.UpArrow)&&(rotationX<maxUpAngle))
        {
            rotateDirection("up");
        }

        if (Input.GetKey(KeyCode.DownArrow)&&(rotationX>minDownAngle))
        {
            rotateDirection("down");
        }

        if (Input.GetKey(KeyCode.LeftArrow)&&(rotationY>-1*maxYAxisAngle))
        {
            rotateDirection("left");
        }

        if (Input.GetKey(KeyCode.RightArrow)&&(rotationY<maxYAxisAngle))
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
                turret.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed, Space.Self);
                break;

            case "down":
                turret.transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed, Space.Self);
                break;
        }
    }
}
