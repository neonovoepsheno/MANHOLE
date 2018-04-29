using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyBehaviour : BallBehaviour
{
    private GameObject target;
    [SerializeField]
    private float eSpeed;
    
    private float currSpeed;
    private float minSpeedSlowMotion;

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


    void SetInitValues()
    {
        target = GameObject.FindGameObjectWithTag("Heart");

        currSpeed = eSpeed;
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
