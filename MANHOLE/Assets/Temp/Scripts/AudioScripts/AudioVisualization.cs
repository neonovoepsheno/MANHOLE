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
    private float[] tempScaleCoefArray;

    private void Start()
    {
        oCurrentScale = this.transform.localScale;
        oMaxScale = oCurrentScale.x * oChangeScaleCoef + oCurrentScale.x;
        oMinScale = oCurrentScale.x - oCurrentScale.x * oChangeScaleCoef;
        tempScaleCoefArray = new float[2];
        pScaleCoefArray = new float[2];
        LinearCoefSelection(oMaxScale, oMinScale, AudioAnalysis.GetMaxSoundCoef(), AudioAnalysis.GetMinSoundCoef(), pScaleCoefArray);
    }


    private void Update()
    {
        ChangeScale();
    }


    private void ChangeScale()
    {
        float newScale = CountNewScaleValue(AudioAnalysis.GetPitchValue());
        if (newScale == 0)
        {
            return;
        }
        this.transform.localScale = new Vector3(newScale, newScale, newScale);
    }


    private float CountNewScaleValue(float soundValue)
    {
        if (soundValue > AudioAnalysis.GetMaxSoundCoef())
        {
            return GetScaleSoundHigher(soundValue);
        }
        else if (soundValue < AudioAnalysis.GetMinSoundCoef())
        {
            return 0;
        }
        else
        {
            return UpdateValueWithLinearCoef(pScaleCoefArray, soundValue);
        }
    }


    private float GetScaleSoundHigher(float soundValue)
    {
        float lowBorder, highBorder;
        float minSoundCoef = AudioAnalysis.GetMinSoundCoef();
        float maxSoundCoef = AudioAnalysis.GetMaxSoundCoef();
        float x = soundValue / maxSoundCoef;
        if (soundValue % maxSoundCoef > 0)
        {
            x++;
        }
        highBorder = (int)x * maxSoundCoef;
        lowBorder = (int)x * minSoundCoef;
        LinearCoefSelection(oMaxScale, oMinScale, highBorder, lowBorder, tempScaleCoefArray);
        return UpdateValueWithLinearCoef(tempScaleCoefArray, soundValue);
    }
}
