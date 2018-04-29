using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemyBehaviour : BallBehaviour
{
    private GameObject target;
    [SerializeField]
    private float beSpeed;
    [SerializeField]
    private float beStartScale;
    [SerializeField]
    private float beFinishScale;
    [SerializeField]
    private float beStartChangeScalePart;

    private float goScale;
    private float startScale;
    private float finishScale;
    private float currSpeed;
    private float minSpeedSlowMotion;

    float currentRadius;
    float currScale;

    float startChangeScaleRadius;

    private float[] eScaleCoefArray;

    private Transform enemyTransform;

    void Start()
    {
        SetInitValues();
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable)
        {
            SimpleEnemyMoving();
            ChangeScaleWhileMoving();
        }
    }


    void SetInitValues()
    {
        target = GameObject.FindGameObjectWithTag("Heart");

        enemyTransform = transform.Find("BlackEnemy");
        goScale = enemyTransform.localScale.x;
        startScale = goScale * beStartScale;
        finishScale = goScale * beFinishScale;
        startChangeScaleRadius = arenaRadius - arenaRadius * beStartChangeScalePart;

        eScaleCoefArray = new float[2];
        LinearCoefSelection(startChangeScaleRadius, 0.0001f, startScale, finishScale, eScaleCoefArray);

        currSpeed = beSpeed;
        minSpeedSlowMotion = SlowMotionAbility.ability.GetMoveSlowStep();
    }


    void ChangeScaleWhileMoving()
    {
        currentRadius = Vector3.Magnitude(target.transform.position - transform.position);
        currScale = startScale;
        if (currentRadius <= startChangeScaleRadius)
        {
            currScale = UpdateValueWithLinearCoef(eScaleCoefArray, currentRadius);
        }
        enemyTransform.localScale = new Vector3(currScale, currScale, currScale);
    }


    void SimpleEnemyMoving()
    {
        if (!SlowMotionAbility.ability.IsAbilityActive())
        {
            if (currSpeed < beSpeed)
            {
                currSpeed += TimeControlManager.timeToDecrease;
            }
            else
            {
                currSpeed = beSpeed;
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
