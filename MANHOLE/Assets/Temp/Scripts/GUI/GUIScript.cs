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


    public static bool isGUIWindowEnable;
    

    void Start()
    {
        InitializeValues();
        EnableStartWindow(true);
        GUIHandler.InitGUIButtons();
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
    }


    public static void EnableStartWindow(bool shouldEnable)
    {
        isGUIWindowEnable = shouldEnable;
        startPanel.SetActive(shouldEnable);
    }


    public static void EnablePauseWindow(bool shouldEnable)
    {
        isGUIWindowEnable = shouldEnable;
        pausePanel.SetActive(shouldEnable);
    }


    void InitializeValues()
    {
        pointsText = GameObject.Find("GamePointsText").GetComponent<Text>();
        pointsText.text = "" + playerPoints;

        losePanel = GameObject.Find("LosePanel");
        pausePanel = GameObject.Find("PausePanel");
        startPanel = GameObject.Find("StartPanel");
    }

    void DisablePanels()
    {
        losePanel.SetActive(false);
        pausePanel.SetActive(false);
    }
}
