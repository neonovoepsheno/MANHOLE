using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GUIHandler : MainManager
{
    public static void SetTriggersButtons()
    {
        SetTrigger(GUIScript.bRestart, OnRestartClick, "bRestart");
        SetTrigger(GUIScript.bStart, OnStartClick, "bStart");
        SetTrigger(GUIScript.bPause, OnPauseClick, "bPause");
        SetTrigger(GUIScript.bContinue, OnContinueClick, "bContinue");
        SetTrigger(GUIScript.bExit, OnExitClick, "bExit");
    }


    public static void OnRestartClick(PointerEventData data)
    {
        GUIScript.gui.SaveRestartState(true);
        SceneManager.LoadScene("main");
    }


    public static void OnExitClick(PointerEventData data)
    {
        Application.Quit();
    }


    public static void OnStartClick(PointerEventData data)
    {
        startTime = GAME_TIME;
        GUIScript.gui.EnableStartWindow(false);
        AudioManager.audioSource.Play();
        AudioManager.isGameStart = true;
    }


    public static void OnPauseClick(PointerEventData data)
    {
        GUIScript.gui.EnablePauseWindow(true);
        AudioManager.IsPause = true;
        TimeControlManager.isPause = true;
    }


    public static void OnContinueClick(PointerEventData data)
    {
        GUIScript.gui.EnablePauseWindow(false);
        AudioManager.IsPause = false;
        TimeControlManager.isPause = false;
    }


    static void SetTrigger(GameObject goButton, Action<PointerEventData> func, string name)
    {
        EventTrigger trigger = goButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { func((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
}
