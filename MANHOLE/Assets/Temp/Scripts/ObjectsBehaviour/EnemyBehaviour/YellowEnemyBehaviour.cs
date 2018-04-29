using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyBehaviour : BallBehaviour
{
    [SerializeField]
    private float yeSpeed;
    [SerializeField]
    private float yeCircleAcceleration;
    [SerializeField]
    private float yeCircleMovingTime;
    //[SerializeField]
    //private float yeStraightMaxSpeed; 50
    //[SerializeField]
    //private float yeStraightMinSpeed; 3

    private GameObject target;

    private float simpleSpeed;
    private float currSpeed;
    private float currCircleSpeed;
    private float minSpeedSlowMotion;
    private float minSpeedSlowMotionCircle;

    //private float[] yeStraightAccelerationCoefArray;

    float currentRadius;

    //const int CIRCLE_PUSH = 1;
    //const int STRAIGHT_PUSH = 2;

    private float currentPitch;
    private float timeDeltaCheck = 1f;
    private float startPushingTime;
    private int currentPushType;
    private float[] pithRangePush = { 50, 70 };

    bool pushState;
    //bool circlePushState;

    private Transform enemyTransform;

    void Start()
    {
        SetInitValues();
    }


    void SetInitValues()
    {
        target = GameObject.FindGameObjectWithTag("Heart");

        enemyTransform = transform.Find("BlackEnemy");

        simpleSpeed = yeSpeed;
        currSpeed = simpleSpeed;
        currCircleSpeed = yeCircleAcceleration;
        minSpeedSlowMotion = SlowMotionAbility.ability.GetMoveSlowStep();
        minSpeedSlowMotionCircle = (yeSpeed / minSpeedSlowMotion) * yeCircleAcceleration;

        startPushingTime = 0;
        pushState = false;

        //yeStraightAccelerationCoefArray = new float[2];
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable)
        {
            YellowEnemyMoving();
            CheckAudioConditionForPush();
        }
    }


    void YellowEnemyMoving()
    {
        if (!SlowMotionAbility.ability.IsAbilityActive())
        {
            if (!pushState)
            {
                if (currSpeed < simpleSpeed)
                {
                    currSpeed += TimeControlManager.timeToDecrease;
                }
                else
                {
                    currSpeed = simpleSpeed;
                }
            }
            else 
            {
                if (currCircleSpeed > yeCircleAcceleration)
                {
                    currCircleSpeed -= TimeControlManager.timeToDecrease;
                }
                else
                {
                    currCircleSpeed = yeCircleAcceleration;
                }
                transform.RotateAround(target.transform.position, Vector3.forward, currCircleSpeed * Time.deltaTime);
                return;
            }
        }
        else
        {
            if (!pushState)
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
            else
            {
                if (currCircleSpeed > minSpeedSlowMotionCircle)
                {
                    currCircleSpeed -= TimeControlManager.timeToDecrease;
                }
                else
                {
                    currCircleSpeed = minSpeedSlowMotionCircle;
                }
                transform.RotateAround(target.transform.position, Vector3.forward, currCircleSpeed * Time.deltaTime);
                return;
            }
        }
        float step = currSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }


    void CheckAudioConditionForPush()
    {
        currentRadius = Vector3.Magnitude(target.transform.position - transform.position);

        if (!pushState)
        {
            if (GAME_TIME - startPushingTime > timeDeltaCheck)
            {
                currentPitch = AudioAnalysis.GetPitchValue();
                if (currentPitch >= pithRangePush[0] && currentPitch <= pithRangePush[1])
                {
                    pushState = true;
                    startPushingTime = GAME_TIME;
                    //currentPushType = Random.Range(1, 3);
                    //if (currentPushType == STRAIGHT_PUSH)
                    //{
                    //    LinearCoefSelection(currentRadius, currentRadius - currentRadius * 0.2f, yeStraightMinSpeed, yeStraightMaxSpeed, yeStraightAccelerationCoefArray);
                    //}
                    //else if (currentPushType == CIRCLE_PUSH)
                    //{
                        
                    //}
                }
            }
        }
        else
        {
            if (GAME_TIME - startPushingTime > yeCircleMovingTime)
            {
                simpleSpeed = yeSpeed;
                pushState = false;
            }
            //if (currentPushType == CIRCLE_PUSH)
            //{
            //    circlePushState = true;
            //    if (GAME_TIME - startPushingTime > 0.2)
            //    {
            //        simpleSpeed = yeSpeed;
            //        circlePushState = false;
            //        pushState = false;
            //    }
            //}
            //else if (currentPushType == STRAIGHT_PUSH)
            //{
            //    simpleSpeed = UpdateValueWithLinearCoef(yeStraightAccelerationCoefArray, currentRadius);
            //    if (simpleSpeed >= yeStraightMaxSpeed)
            //    {
            //        simpleSpeed = yeSpeed;
            //        pushState = false;
            //    }
            //}
        }
    }
}
