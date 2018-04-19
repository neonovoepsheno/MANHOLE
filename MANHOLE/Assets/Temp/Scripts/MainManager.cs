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
    protected static float startTime;


    private void Start()
    {
        playerPoints = 0;
        isLose = false;
        SpiralMoving.InitSpiralMoving();
        startTime = 0;
    }


    protected static void IncrementingPlayerPoints()
    {
        playerPoints++;
    }
}
