using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : BallBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float pOuterSpeed;
	[SerializeField]
	private float pOuterSpeedSpiral;
    [SerializeField]
    private float pInnerSpeed;
    [SerializeField]
    private float pInnerScale;
    [SerializeField]
    private float pSpiralSpeed;
    [SerializeField]
    private float pOuterRadius;
    [SerializeField]
    private float pInnerRadius;
    [SerializeField]
    private float pStartDelay;
    [SerializeField]
    private float pSpiralAllowedTime;
    [SerializeField]
    private float pSpiralAllowedDelayTime;

    private static int directionCoef = 1;

    private float radius;
    private Vector3 startScale;

    private float pTimeCounter;
    private float pCurrentSpeed;
    private float x;
    private float y;
    
	private float[] pSpeedSpiralCoefArray;
    private float[] pScaleCoefArray;
    
    public static float spiralStartTime;
    public static float spiralFinishTime;

    void Start()
    {
        InitializeValues();
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable && GAME_TIME > pStartDelay)
        {
            float temp_coef = Time.deltaTime * pCurrentSpeed * directionCoef;
            pTimeCounter += temp_coef;
            PlayerMove();
        }
    }


    private void InitializeValues()
    {
        pTimeCounter = 0;
        startScale = transform.localScale;
        radius = pOuterRadius;
        isLose = false;
        pCurrentSpeed = 0;
        spiralStartTime = 0;
        spiralFinishTime = 0;

        pScaleCoefArray = new float[2];
        LinearCoefSelection(startScale.x, pInnerScale, pOuterRadius, pInnerRadius, pScaleCoefArray);
        pSpeedSpiralCoefArray = new float[2];
        LinearCoefSelection(pOuterSpeedSpiral, pInnerSpeed, pOuterRadius, pInnerRadius, pSpeedSpiralCoefArray);
    }


    private void PlayerMove()
    {
        if (!SpiralMoving.isSpiral)
        {
            float new_radius = radius + pSpiralSpeed;
            if (new_radius > pOuterRadius)
            {
                UpdateProperty(pOuterRadius);
            }
            else
            {
                UpdateProperty(new_radius);
            }
        }
        else
        {
            float new_radius = radius - pSpiralSpeed;
            if (new_radius > pInnerRadius)
            {
                UpdateProperty(new_radius);
            }
        }
        x = Mathf.Cos(pTimeCounter)  * radius;
        y = Mathf.Sin(pTimeCounter) * radius;
     
        transform.position = new Vector3(x, y, 0);
    }


    private void UpdateProperty(float radius)
    {
		UpdateScale (radius);
		UpdateSpeed (radius);
        this.radius = radius;
    }


	protected void UpdateScale(float radius)
	{
		float currScale = UpdateValueWithLinearCoef(pScaleCoefArray, radius);
		transform.localScale = new Vector3(currScale, currScale, currScale);
	}


	protected void UpdateSpeed(float radius)
	{
        if (radius < pOuterRadius)
        {
            pCurrentSpeed = UpdateValueWithLinearCoef(pSpeedSpiralCoefArray, radius);
        }
        else
        {
            pCurrentSpeed = pOuterSpeed;
        }
	}


    public void ChangePlayerDirection()
    {
        directionCoef *= -1;
    }


    public float GetCurrentPlayerRadius()
    {
        return radius;
    }


    public float GetPlayerOuterRadius()
    {
        return pOuterRadius;
    }

    
    public float GetPlayerSpiralAllowedTime()
    {
        return pSpiralAllowedTime;
    }


    public float GetPlayerSpiralAllowedDelayTime()
    {
        return pSpiralAllowedDelayTime;
    }
}
