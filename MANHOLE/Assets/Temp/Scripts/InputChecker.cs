using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputChecker : BallBehaviour {

    private PlayerBehaviour playerBehavior;
    private const float HOLD_TIME = 0.25f; 
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
        if (Input.GetButton("Fire1") && IsValidInput())
        {
            acumTime += Time.deltaTime;
            if (acumTime >= HOLD_TIME)
            {
                if (SpiralMoving.isSpiralStartAllowed)
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
                }
                return;
            }

        }
        else if (Input.GetButtonUp("Fire1") && IsValidInput())
        {
            if (acumTime < HOLD_TIME)
            {
                if (!SpiralMoving.IsPlayerOnSpiral())
                {
                    playerBehavior.ChangePlayerDirection();
                }
            }
            acumTime = 0;
        }
        if (SpiralMoving.isSpiral)
        {
            PlayerBehaviour.spiralFinishTime = GAME_TIME;
        }
        SpiralMoving.isSpiral = false;
    }

    bool IsValidInput()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return false;
        else
            return true;
    }
}
