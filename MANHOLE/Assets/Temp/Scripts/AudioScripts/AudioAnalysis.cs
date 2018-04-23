using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioAnalysis : AudioManager
{
    private const int SAMPLES_LENGTH = 1024;

    private static float MAX_SOUND_COEF = 2500f;
    private static float MIN_SOUND_COEF = 100f;

    private static float[] samples = new float[SAMPLES_LENGTH];
    private static float[] spectrum = new float[SAMPLES_LENGTH];
    private static float fSample;
  
    private static float rmsValue, dbValue, pitchValue;
    private float refValue = 0.1f;
    private float threshold = 0.02f;

    private void Start()
    {
        fSample = AudioSettings.outputSampleRate;
    }


    private void Update()
    {
        GetSpectrumAudioSource();
        MakeFreqBands();
    }


    private void GetSpectrumAudioSource()
    {
        audioSource.GetOutputData(samples, 0);
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }


    private void MakeFreqBands()
    {
        int i;
        float sum = 0;
        for (i = 0; i < SAMPLES_LENGTH; i++)
        {
            sum += Mathf.Pow(2, samples[i]);
        }
        rmsValue = Mathf.Sqrt(sum / SAMPLES_LENGTH);
        dbValue = 20 * Mathf.Log10(rmsValue / refValue);
        if (dbValue < -160)
        {
            dbValue = -160;
        }
        float maxV = 0;
        int maxN = 0;
        for (i = 0; i < SAMPLES_LENGTH; i++)
        {
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                maxV = spectrum[i];
                maxN = i;
            }
        }
        float freqN = maxN;
        float dR, dL;
        if (maxN > 0 && maxN < SAMPLES_LENGTH - 1)
        {
            dL = spectrum[maxN - 1] / spectrum[maxN];
            dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchValue = freqN * (fSample / 2) / SAMPLES_LENGTH;
        if (pitchValue > 70 && pitchValue < 100)
        {
           //
        }
        Debug.Log(dbValue);
    }


    public static float GetBandBuffer()
    {
        return pitchValue;
    }


    public static float GetMaxSoundCoef()
    {
        return MAX_SOUND_COEF;
    }


    public static float GetMinSoundCoef()
    {
        return MIN_SOUND_COEF;
    }
}
