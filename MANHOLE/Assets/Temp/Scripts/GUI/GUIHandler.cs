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
    }

    public static void OnRestartClick(PointerEventData data)
    {
        SceneManager.LoadScene("main");
    }

    public static void OnStartClick(PointerEventData data)
    {
        GUIScript.EnableStartWindow(false);
    }

    public static void OnPauseClick(PointerEventData data)
    {
        if (!GUIScript.isGUIWindowEnable)
        {
            GUIScript.EnablePauseWindow(true);
        }
    }


    public static void OnContinueClick(PointerEventData data)
    {
        GUIScript.EnablePauseWindow(false);
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
