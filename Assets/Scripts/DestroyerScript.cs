using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyerScript : Ship  // INHERITANCE
{
    private float speedMin = 16;
    private float speedMax = 23;
    public override void changeSpeed()
    {
        speed = Random.Range(speedMin, speedMax);
    }
    protected override void setValues() // POLYMORPHISM
    {
        sinkingSpeed = 2;
        changeSpeed();
        sankingDepth = -11;
        maxXPosition = 200;
        xFromZPosition = 110;
        yPosition = -2.5f;
        zMinPosition = 50;
        zMaxPosition = 90;
        scorePerHit = 2;
    }
    protected override void setShipHealth()
    {
        playerControllerScript.destroyerHealth = shipHealth;        
    }
    
}
