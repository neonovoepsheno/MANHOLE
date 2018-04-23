using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMoving : BallBehaviour
{
    public static bool isSpiral = false;
    public static bool isSpiralStartAllowed = true;

    private static PlayerBehaviour player;

    public static void InitSpiralMoving()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    

    public static bool IsSpiralAllowed()
    {

        if (SpiralMoving.isSpiral)
        {
            if (GAME_TIME - PlayerBehaviour.spiralStartTime > player.GetPlayerSpiralAllowedTime() || !GUIScript.CheckSpiralBarCondition())
            {
                PlayerBehaviour.spiralFinishTime = GAME_TIME;
                return false;
            }
        }
        return true;
    }


    public static bool IsSpiralStartAllowed()
    {
        isSpiralStartAllowed = (GAME_TIME - PlayerBehaviour.spiralFinishTime > player.GetPlayerSpiralAllowedDelayTime() && GUIScript.CheckSpiralBarCondition());
        return isSpiralStartAllowed;
    }


    public static bool IsPlayerOnSpiral()
    {
        if (player.GetCurrentPlayerRadius() < player.GetPlayerOuterRadius())
        {
            return true;
        }
        return false;
    }
}
