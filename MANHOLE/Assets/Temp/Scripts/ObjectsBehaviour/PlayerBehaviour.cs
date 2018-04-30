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
    private float pSpiralAllowedTime;
    [SerializeField]
    private float pSpiralAllowedDelayTime;
	[SerializeField]
	private float pOuterScale;
    [SerializeField]
    private float pOuterSpeedBack;
    [SerializeField]
    private float pInnerSpeedBack;

    private static int directionCoef = 1;

    private float radius;

    private float pTimeCounter;
    private float pCurrentSpeed;
    private float spiralBarVisualDelta;
    private float innerScale;
    private float outerScale;
    private float x;
    private float y;

	private Vector3 hPosition;
    private Vector3 startScale;

    private float[] pSpeedSpiralCoefArray;
    private float[] pScaleCoefArray;
    private float[] pSpeedBackCoefArray;
    
    public static float spiralStartTime;
    public static float spiralFinishTime;

    void Start()
    {
        InitializeValues();
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable)
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
        innerScale = pInnerScale * transform.localScale.x;
        outerScale = pOuterScale * transform.localScale.x;
        radius = pOuterRadius;
        isLose = false;
        pCurrentSpeed = 0;
        spiralStartTime = 0;
        spiralFinishTime = 0;
		hPosition = target.transform.position;

        pScaleCoefArray = new float[2];
        LinearCoefSelection(pOuterRadius, pInnerRadius, startScale.x, innerScale, pScaleCoefArray);
        pSpeedSpiralCoefArray = new float[2];
        LinearCoefSelection(pOuterRadius, pInnerRadius, pOuterSpeedSpiral, pInnerSpeed, pSpeedSpiralCoefArray);
        pSpeedBackCoefArray = new float[2];
        LinearCoefSelection(pOuterRadius, pInnerRadius, pOuterSpeedSpiral, pInnerSpeed, pSpeedBackCoefArray);


        transform.localScale = new Vector3 (outerScale, outerScale, outerScale);
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
            GUIScript.gui.ChangeSpiralBarValue((GUIScript.gui.GetSpiralBarMaxValue() * pSpiralSpeed) / pOuterRadius);
        }
        x = Mathf.Cos(pTimeCounter)  * radius;
        y = Mathf.Sin(pTimeCounter) * radius;
     
		transform.position = new Vector3(x + hPosition.x, y + hPosition.y, 0);
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
            if (SpiralMoving.isSpiral)
            {
                pCurrentSpeed = UpdateValueWithLinearCoef(pSpeedSpiralCoefArray, radius);
            }
            else
            {
                pCurrentSpeed = UpdateValueWithLinearCoef(pSpeedBackCoefArray, radius);
            }
           
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
