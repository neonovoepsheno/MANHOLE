﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChecker : BallBehaviour {

    private PlayerBehaviour playerBehavior;
    private const float HOLD_TIME = 0.4f; 
    private float acumTime = 0;

    private void Start()
    {
        playerBehavior = new PlayerBehaviour();
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable)
        {
            CheckPressDown();
            CheckLongPress();
        }
    }


    void CheckPressDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (SpiralMoving.IsSpiralStartAllowed())
            {
                PlayerBehaviour.spiralStartTime = GAME_TIME;
            }
        }
    }


    void CheckLongPress()
    {
        if (Input.GetButton("Fire1"))
        {
            if (SpiralMoving.isSpiralStartAllowed)
            {
                acumTime += Time.deltaTime;
                if (acumTime >= HOLD_TIME)
                {
                    if (!SpiralMoving.isSpiral)
                    {
                        if (SpiralMoving.IsSpiralStartAllowed())
                        {
                            SpiralMoving.isSpiral = SpiralMoving.IsSpiralAllowed();
                        }
                    }
                    else
                    {
                        SpiralMoving.isSpiral = SpiralMoving.IsSpiralAllowed();
                    }
                    return;
                }
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (acumTime < HOLD_TIME)
            {
                //Debug.Log("Click");
                playerBehavior.ChangePlayerDirection();
            }
            acumTime = 0;
        }
        if (SpiralMoving.isSpiral)
        {
            PlayerBehaviour.spiralFinishTime = GAME_TIME;
        }
        SpiralMoving.isSpiral = false;
    }
}