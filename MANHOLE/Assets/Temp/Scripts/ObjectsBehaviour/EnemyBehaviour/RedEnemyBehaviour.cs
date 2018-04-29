using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyBehaviour : BallBehaviour
{
    private GameObject target;
    [SerializeField]
    private float eSpeed;
	[SerializeField]
	private GameObject plusOne;

    private float startScale;
    private float currSpeed;
    private float minSpeedSlowMotion;

    private float[] eScaleCoefArray;

    private void Start()
    {
        SetInitValues();
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable)
        {
            SimpleEnemyMoving();
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            TouchPlayer();
        }
        else if (coll.gameObject.tag == "Heart")
        {
            TouchHeart();
        }
    }


    void TouchPlayer()
    {
        Instantiate(plusOne, transform.position, Quaternion.identity);
        Destroy(gameObject);
        IncrementingPlayerPoints(1);
        IncrementingPlayerCombo(1);
        GUIScript.gui.UpdateGamePointsVisual();
        GUIScript.gui.UpdateComboVisual();
        GUIScript.gui.ChangeSpiralBarValue(-1 * GUIScript.gui.GetComboDelta());
    }


    void TouchHeart()
    {
        if (IsDeathMode())
        {
            GUIScript.gui.ShowLoseWindow();
            isLose = true;
            AudioManager.IsPause = true;
        }
        playerCombo = 0;
        GUIScript.gui.UpdateComboVisual();
        GUIScript.gui.SetSpiralBarValue(0);
        Destroy(gameObject);
    }


    void SetInitValues()
    {
        target = GameObject.FindGameObjectWithTag("Heart");

        startScale = transform.localScale.x;currSpeed = eSpeed;
        minSpeedSlowMotion = SlowMotionAbility.ability.GetMoveSlowStep();
    }


    void SimpleEnemyMoving()
    {
        if (!SlowMotionAbility.ability.IsAbilityActive())
        {
            if (currSpeed < eSpeed)
            {
                currSpeed += TimeControlManager.timeToDecrease;
            }
            else
            {
                currSpeed = eSpeed;
            }
        }
        else
        {
            if (currSpeed > minSpeedSlowMotion)
            {
                currSpeed -= TimeControlManager.timeToDecrease;
            }
            else
            {
                currSpeed = minSpeedSlowMotion;
            }
        }
        float step = currSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
}
