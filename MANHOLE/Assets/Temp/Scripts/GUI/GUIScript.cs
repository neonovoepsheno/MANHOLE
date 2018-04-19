using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIScript : MainManager
{
    static Text pointsText;
   
    static GameObject losePanel;
    static GameObject startPanel;
    static GameObject pausePanel;

    public static GameObject bRestart;
    public static GameObject bStart;
    public static GameObject bContinue;
    public static GameObject bPause;


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
    }


    public static void EnableStartWindow(bool shouldEnable)
    {
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
        pointsText = GameObject.Find("GamePointsText").GetComponent<Text>();
        pointsText.text = "" + playerPoints;

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
    }
}
