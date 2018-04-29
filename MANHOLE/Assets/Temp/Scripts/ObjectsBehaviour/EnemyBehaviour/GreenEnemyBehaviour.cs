using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyBehaviour : BallBehaviour
{
    [SerializeField]
    float geFirstSpeed;
    [SerializeField]
    float geSecondtSpeed;
    [SerializeField]
    float geFirstScale;
    [SerializeField]
    float geSecondtScale;

    GameObject target;
    Transform child;

    float currRadius;
    float deltaRadius;

    float startScale;
    float firstScale;
    float secondScale;
    float currScale;

    float currSpeed;
    float targetSpeed;
    float minSpeedSlowMotion;

    float[] geScaleCoefArray;
    float[] geSpeedCoefArray;

    int touchPlayerCounter;


    void Start()
    {
        SetInitValues();
    }


    void SetInitValues()
    {
        target = GameObject.FindGameObjectWithTag("Heart");
        child = transform.Find("GreenEnemy");

        startScale = child.transform.localScale.x;
        firstScale = geFirstScale * startScale;
        secondScale = geSecondtScale * startScale;

        child.transform.localScale = new Vector3(firstScale, firstScale, firstScale);

        touchPlayerCounter = 0;

        minSpeedSlowMotion = SlowMotionAbility.ability.GetMoveSlowStep();
        targetSpeed = geFirstSpeed;

        geScaleCoefArray = new float[2];
        geSpeedCoefArray = new float[2];
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable)
        {
            EnemyMoving();
            UpdateScaleSpeedValues();
        }
    }


    void EnemyMoving()
    {
        if (!SlowMotionAbility.ability.IsAbilityActive())
        {
            if (currSpeed < targetSpeed)
            {
                currSpeed += TimeControlManager.timeToDecrease;
            }
            else
            {
                currSpeed = targetSpeed;
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
        currRadius = Vector3.Magnitude(target.transform.position - transform.position);
    }


    void UpdateScaleSpeedValues()
    {
        if (touchPlayerCounter == 1 && currRadius >= deltaRadius)
        {
            targetSpeed = UpdateValueWithLinearCoef(geSpeedCoefArray, currRadius);
            currScale = UpdateValueWithLinearCoef(geScaleCoefArray, currRadius);
            child.localScale = new Vector3(currScale, currScale, currScale);
        }
    }


    public void FirstPlayerTouch()
    {
        deltaRadius = (float)(currRadius - currRadius * 0.05);
        LinearCoefSelection(currRadius, deltaRadius, firstScale, secondScale, geScaleCoefArray);
        LinearCoefSelection(currRadius, deltaRadius, geFirstSpeed, geSecondtSpeed, geSpeedCoefArray);
    }


    public int GetNumPlayersTouch()
    {
        return touchPlayerCounter;
    }


    public void IncrementNumPlayersTouch()
    {
        touchPlayerCounter++;
    }
}
