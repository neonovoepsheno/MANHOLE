using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MainManager
{
    protected void LinearCoefSelection(float startValueProperty, float endValueProperty, float startValue, float endValue, float[] coefArray)
    {
        float val_first, val_second, val_third;
        val_first = startValue - endValue;
        val_second = startValueProperty - endValueProperty;
        val_third = startValue * endValueProperty - endValue * startValueProperty;
        coefArray[0] = val_second / val_first;
        coefArray[1] = val_third / val_first;
    }


    protected float UpdateValueWithLinearCoef(float[] coefArray, float value)
    {
        return value * coefArray[0] + coefArray[1];
    }
}
