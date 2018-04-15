using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    protected static float GAME_TIME;
    protected static int playerPoints;
    protected static bool isLose;


    private void Start()
    {
        playerPoints = 0;
        GAME_TIME = 0f;
        isLose = false;
        SpiralMoving.InitSpiralMoving();
        GUIScript.EnableStartWindow(true);
    }


    private void Update()
    {
        if (!isLose && !GUIScript.isGUIWindowEnable)
        {
            GAME_TIME += Time.deltaTime;
        }
    }


    protected static void IncrementingPlayerPoints()
    {
        playerPoints++;
    }
}
