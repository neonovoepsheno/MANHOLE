using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIScript : MainManager
{
    private static float spiralBarMaxValue = 5;

    static Text pointsText;
    static Text scoreText;
    static Text trackNameText;

    static GameObject losePanel;
    static GameObject startPanel;
    static GameObject pausePanel;

    public static GameObject bRestart;
    public static GameObject bStart;
    public static GameObject bContinue;
    public static GameObject bPause;

    public static Slider barSpiral;

    public static bool isGUIWindowEnable;
    public static bool isGUIWindowPauseEnable;


    void Start()
    {
        InitializeValues();
        EnableStartWindow(true);
        GUIHandler.SetTriggersButtons();
        DisablePanels();
    }


    public static void UpdateGamePointsVisual()
    {
        pointsText.text = "" + playerPoints;
    }


    public static void ShowLoseWindow()
    {
        isGUIWindowEnable = true;
        losePanel.SetActive(true);
        EnableUIgo(false);
        scoreText.text = "Score: " + playerPoints;
        trackNameText.text = AudioManager.GetTrackName();
    }


    public static void EnableStartWindow(bool shouldEnable)
    {
        if (shouldEnable)
        {
            if (PlayerPrefs.GetInt(restartPlayerPref) != null && PlayerPrefs.GetInt(restartPlayerPref) == 1)
            {
                startTime = GAME_TIME;
                GUIScript.EnableStartWindow(false);
                AudioManager.audioSource.Play();
                AudioManager.isGameStart = true;
                return;
            }
        }
        isGUIWindowEnable = shouldEnable;
        startPanel.SetActive(shouldEnable);
        EnableUIgo(!shouldEnable);
    }


    public static void EnablePauseWindow(bool shouldEnable)
    {
        //isGUIWindowEnable = shouldEnable;
        if (shouldEnable)
        {
            isGUIWindowPauseEnable = shouldEnable;
        }
        pausePanel.SetActive(shouldEnable);
        EnableUIgo(!shouldEnable);
    }


    void InitializeValues()
    {
        barSpiral = GameObject.Find("SpiralBar").GetComponent<Slider>();
        barSpiral.value = spiralBarMaxValue;

        pointsText = GameObject.Find("GamePointsText").GetComponent<Text>();
        pointsText.text = "" + playerPoints;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        trackNameText = GameObject.Find("TrackName").GetComponent<Text>();

        losePanel = GameObject.Find("LosePanel");
        pausePanel = GameObject.Find("PausePanel");
        startPanel = GameObject.Find("StartPanel");

        bRestart = GameObject.Find("bRestart");
        bStart = GameObject.Find("bStart");
        bPause = GameObject.Find("bPause");
        bContinue = GameObject.Find("bContinue");
    }

    void DisablePanels()
    {
        losePanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    static void EnableUIgo(bool enable)
    {
        bPause.SetActive(enable);
        barSpiral.gameObject.SetActive(enable);
    }


    public static void ChangeSpiralBarValue(float delta)
    {
        if (barSpiral.value - delta > 0 || barSpiral.value - delta < spiralBarMaxValue)
        {
            barSpiral.value -= delta;
        }
        else if (barSpiral.value - delta < spiralBarMaxValue)
        {
            barSpiral.value = spiralBarMaxValue;
        }
        else if (barSpiral.value - delta > 0)
        {
            barSpiral.value = 0;
        }
    }


    public static float GetSpiralBarMaxValue()
    {
        return spiralBarMaxValue;
    }


    public static bool CheckSpiralBarCondition()
    {
        if (barSpiral.value == 0)
        {
            return false;
        }
        return true;
    }
}
