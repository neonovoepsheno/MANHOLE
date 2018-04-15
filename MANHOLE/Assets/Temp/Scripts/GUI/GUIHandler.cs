using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GUIHandler : GUIScript
{
    GameObject bRestart;
    GameObject bStart;
    GameObject bContinue;
    GameObject bPause;

    void Start()
    {
        SetInitValues();
    }

    void SetInitValues()
    {
        bRestart = GameObject.Find("bRestart");
        SetTrigger(bRestart, OnRestartClick);

        Debug.Log(bRestart.activeSelf);

        bStart = GameObject.Find("bStart");
        SetTrigger(bStart, OnStartClick);

        bPause = GameObject.Find("bPause");
        SetTrigger(bPause, OnPauseClick);

        bContinue = GameObject.Find("bContinue");
        SetTrigger(bContinue, OnContinueClick);

        DisablePanels();
    }

    public void OnRestartClick(PointerEventData data)
    {
        SceneManager.LoadScene("main");
    }

    public void OnStartClick(PointerEventData data)
    {
        EnableStartWindow(false);
    }

    public void OnPauseClick(PointerEventData data)
    {
        if (!isGUIWindowEnable)
        {
            EnablePauseWindow(true);
        }
    }


    public void OnContinueClick(PointerEventData data)
    {
        EnablePauseWindow(false);
    }

    void SetTrigger(GameObject goButton, Action<PointerEventData> func)
    {
        EventTrigger trigger = goButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { func((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
}
