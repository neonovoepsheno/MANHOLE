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
    protected static Text comboText;

    static GameObject losePanel;
    static GameObject startPanel;
    static GameObject pausePanel;

    public static GameObject bRestart;
    public static GameObject bStart;
    public static GameObject bContinue;
    public static GameObject bPause;
    public static GameObject bExit;

    public static Slider barSpiral;

    static Toggle deathModeChecker;

    GUIComboVisual guiCombo;

    public static bool isGUIWindowEnable;
    public static bool isGUIWindowPauseEnable;

    float spiralBarDeltaCounts;


    public void UpdateGamePointsVisual()
    {
        bool playerPointsCondition = playerPoints != 0;
        pointsText.gameObject.SetActive(playerPointsCondition);
        pointsText.text = "" + playerPoints;
    }


    public void UpdateComboVisual()
    {
        guiCombo.UpdateVisual();
    }


    public void ShowLoseWindow()
    {
        isGUIWindowEnable = true;
        losePanel.SetActive(true);
        EnableUIgo(false);
        scoreText.text = "Score: " + playerPoints;
        trackNameText.text = AudioManager.GetTrackName();
    }


    public void EnableStartWindow(bool shouldEnable)
    {
        if (shouldEnable)
        {
            if (PlayerPrefs.GetInt(restartPlayerPref) == 1)
            {
                startTime = GAME_TIME;
                GUIScript.gui.EnableStartWindow(false);
                AudioManager.audioSource.Play();
                AudioManager.isGameStart = true;
                return;
            }
        }
        isGUIWindowEnable = shouldEnable;
        startPanel.SetActive(shouldEnable);
        EnableUIgo(!shouldEnable);
        GUIScript.gui.SaveRestartState(false);
    }


    public  void EnablePauseWindow(bool shouldEnable)
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
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        trackNameText = GameObject.Find("TrackName").GetComponent<Text>();
        comboText = GameObject.Find("ComboText").GetComponent<Text>();

        losePanel = GameObject.Find("LosePanel");
        pausePanel = GameObject.Find("PausePanel");
        startPanel = GameObject.Find("StartPanel");

        bRestart = GameObject.Find("bRestart");
        bStart = GameObject.Find("bStart");
        bPause = GameObject.Find("bPause");
        bContinue = GameObject.Find("bContinue");
        bExit = GameObject.Find("bExit");

        deathModeChecker = GameObject.Find("DeathModeChecker").GetComponent<Toggle>();
        deathModeChecker.isOn = false;

        guiCombo = comboText.GetComponent<GUIComboVisual>();
        spiralBarDeltaCounts = guiCombo.GetMin() - 1;
        guiCombo.InitVal();
    }


    void DisablePanels()
    {
        losePanel.SetActive(false);
        pausePanel.SetActive(false);
    }


    void EnableUIgo(bool enable)
    {
        bPause.SetActive(enable);
        barSpiral.gameObject.SetActive(enable);
    }


    public void ChangeSpiralBarValue(float delta)
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


    public void SetSpiralBarValue(float value)
    {
        if (value >= 0 && value <= spiralBarMaxValue)
        {
            barSpiral.value = value;
        }
    }


    public float GetSpiralBarMaxValue()
    {
        return spiralBarMaxValue;
    }


    public bool CheckSpiralBarCondition()
    {
        if (barSpiral.value == 0)
        {
            return false;
        }
        return true;
    }


    public void SaveRestartState(bool is_restart)
    {
        if (is_restart)
        {
            PlayerPrefs.SetInt(restartPlayerPref, 1);
        }
        else
        {
            PlayerPrefs.SetInt(restartPlayerPref, 0);
        }
    }


    public bool GetDeathModeToggleValue()
    {
        return deathModeChecker.isOn;
    }


    public int GetComboDelta()
    {
        if (playerCombo >= guiCombo.GetMin())
        {
            //return (int)(playerCombo - spiralBarDeltaCounts);
            return 1;
        }
        return 0;
    }

    //singleton
    public static GUIScript gui = null;

    void Start()
    {
        if (gui == null)
        {
            gui = this;
            InitializeValues();
            gui.UpdateGamePointsVisual();
            gui.UpdateComboVisual();
            gui.EnableStartWindow(true);
            GUIHandler.SetTriggersButtons();
            DisablePanels();
        }
        else if (gui == this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        guiCombo.CheckComboAnimator();
    }
}
