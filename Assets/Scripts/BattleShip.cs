using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShip : Ship
{
   
    protected override void setValues()
    {
        sinkingSpeed = 2.3f;
        speed = 20f;
        sankingDepth = -20;
        maxXPosition = 250;
        xFromZPosition = 160;
        yPosition = -2.8f;
        zMinPosition = 100;
        zMaxPosition = 160;
        scorePerHit = 1;
    }
    protected override void setShipHealth()
    {
        playerControllerScript.battleShipHealth = shipHealth;
    }
    
}

