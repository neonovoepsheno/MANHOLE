using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIScript : MainManager
{
    protected static Text pointsText;
    protected static GameObject losePanel;
    protected static GameObject startPanel;
    protected static GameObject pausePanel;


    public static bool isGUIWindowEnable;
    

    void Start()
    {
        InitializeValues();
    }


    public static void UpdateGamePointsVisual()
    {
        pointsText.text = "" + playerPoints;
    }


    public static void ShowLoseWindow()
    {
        Debug.Log("ShowLoseWindow");
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


    private void InitializeValues()
    {
        pointsText = GameObject.Find("GamePointsText").GetComponent<Text>();
        pointsText.text = "" + playerPoints;

        losePanel = GameObject.Find("LosePanel");
        pausePanel = GameObject.Find("PausePanel");
        startPanel = GameObject.Find("StartPanel");
    }


    protected void DisablePanels()
    {
        losePanel.SetActive(false);
        pausePanel.SetActive(false);
    }
}
