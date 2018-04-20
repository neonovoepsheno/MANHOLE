using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualization : BallBehaviour
{
    [SerializeField]
    private int bufferBandN;
    [SerializeField]
    private float oChangeScaleCoef;

    private Vector3 oCurrentScale;
    private float oMaxScale;
    private float oMinScale;
    private float[] pScaleCoefArray;

    private void Start()
    {
        oCurrentScale = this.transform.localScale;
        oMaxScale = oCurrentScale.x * oChangeScaleCoef + oCurrentScale.x;
        oMinScale = oCurrentScale.x - oCurrentScale.x * oChangeScaleCoef;
        pScaleCoefArray = new float[2];
        LinearCoefSelection(oMaxScale, oMinScale, AudioAnalysis.GetMaxSoundCoef(), AudioAnalysis.GetMinSoundCoef(), pScaleCoefArray);
    }


    private void Update()
    {
        ChangeScale();
    }


    private void ChangeScale()
    {
        float newScale = CountNewScaleValue(AudioAnalysis.GetBandBuffer());
        this.transform.localScale = new Vector3(newScale, newScale, newScale);
    }


    private float CountNewScaleValue(float soundValue)
    {
        if (soundValue > AudioAnalysis.GetMaxSoundCoef())
        {
            return oMaxScale;
        }
        else if (soundValue < AudioAnalysis.GetMinSoundCoef())
        {
            return oMinScale;
        }
        else
        {
            return UpdateValueWithLinearCoef(pScaleCoefArray, soundValue);
        }
    }
}
