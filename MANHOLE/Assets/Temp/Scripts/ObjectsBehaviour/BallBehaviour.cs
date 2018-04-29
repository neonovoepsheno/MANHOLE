using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MainManager
{
    protected void LinearCoefSelection(float x1, float y1, float x2, float y2, float[] coefArray)
    {
        float val_first, val_second, val;
        val = x1 - y1;
        val_first = x2 - y2;
        val_second = x1 * y2 - x2 * y1;
        coefArray[0] = val_first / val;
        coefArray[1] = val_second / val;
    }


    protected float UpdateValueWithLinearCoef(float[] coefArray, float value)
    {
        return value * coefArray[0] + coefArray[1];
    }
}
