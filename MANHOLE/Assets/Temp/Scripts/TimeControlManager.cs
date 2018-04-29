using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlManager : MainManager
{
    [SerializeField]
    private float tStartDelay;
    [SerializeField]
    private float tTimeToDecreasePause;
    [SerializeField]
    private float minTimeScale;

    private float maxTimeScale = 1f;
    private float currTimeScale;

    public static float startDelay;
    public static float timeToDecrease;
    public static bool isPause;

    public static TimeControlManager timeControlManager = null;

    void Start()
    {
        if (timeControlManager == null)
        {
            timeControlManager = this;
            startDelay = tStartDelay;
            timeToDecrease = tTimeToDecreasePause;
            currTimeScale = maxTimeScale;
        }
        else if (timeControlManager == this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (GUIScript.isGUIWindowPauseEnable)
        {
            CreateSlowMotionPause();
        }
    }

    public void CreateSlowMotionPause()
    {
        if (isPause)
        {
            if (currTimeScale > minTimeScale)
            {
                currTimeScale -= TimeControlManager.timeToDecrease;
            }
            if (currTimeScale < minTimeScale)
            {
                currTimeScale = minTimeScale;
            }
        }
        else
        {
            if (currTimeScale < maxTimeScale)
            {
                currTimeScale += TimeControlManager.timeToDecrease;
            }
            if (currTimeScale > maxTimeScale)
            {
                GUIScript.isGUIWindowPauseEnable = false;
                currTimeScale = maxTimeScale;
            }
        }
        Time.timeScale = currTimeScale;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
