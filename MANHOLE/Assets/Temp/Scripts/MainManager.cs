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
    protected static float startTime;
    protected static float arenaRadius;

    protected static int playerPoints;
    protected static int playerCombo;

    protected static bool isLose;

    protected static string restartPlayerPref = "IfFromRestart";

    public static MainManager manager = null;


    private void Start()
    {
        if (manager == null)
        {
            manager = this;
            Initialize();
        }
        else if (manager == this)
        {
            Destroy(gameObject);
        }
    }


    void Initialize()
    {
        playerPoints = 0;
        isLose = false;
        SpiralMoving.InitSpiralMoving();
        startTime = 0;
        playerCombo = 0;
        arenaRadius = 2.45f;
    }


    protected static void IncrementingPlayerPoints(int delta)
    {
        playerPoints += delta;
    }


    protected static void IncrementingPlayerCombo(int delta)
    {
        playerCombo += delta;
    }


    protected static bool IsDeathMode()
    {
        return GUIScript.gui.GetDeathModeToggleValue();
    }
}
