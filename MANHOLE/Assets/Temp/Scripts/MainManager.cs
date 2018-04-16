using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    protected static float GAME_TIME
    {
        get
        {
            return Time.time;
        }
    }
    protected static int playerPoints;
    protected static bool isLose;


    private void Start()
    {
        playerPoints = 0;
        isLose = false;
        SpiralMoving.InitSpiralMoving();
    }


    protected static void IncrementingPlayerPoints()
    {
        playerPoints++;
    }
}
