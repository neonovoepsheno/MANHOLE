using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioAnalysis : AudioManager
{
    private const int FREQ_BANDS_LENGTH = 8;
    private const int SAMPLES_LENGTH = 512;

    private static float MAX_SOUND_COEF = 10;
    private static float MIN_SOUND_COEF = 0.5f;

    [SerializeField]
    private float min;
    [SerializeField]
    private float delta;

    private static float[] samples = new float[SAMPLES_LENGTH];
    private static float[] freqBand = new float[FREQ_BANDS_LENGTH];
    private static float[] bandBuffer = new float[FREQ_BANDS_LENGTH];
    private float[] bufferDecrease = new float[FREQ_BANDS_LENGTH];


    private void Update()
    {
        GetSpectrumAudioSource();
        MakeFreqBands();
        BandBuffer();
    }


    private void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }


    private void MakeFreqBands()
    {
        int count = 0;

        for (int i = 0; i < FREQ_BANDS_LENGTH; i++)
        {
            float average = 0;
            int sampleCounter = (int)(Mathf.Pow(2, i) * 2);

            if (i == 7)
            {
                sampleCounter += 2;
            }

            for (int j = 0; j < sampleCounter; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            average /= count;
            freqBand[i] = average * 10;
        }
    }


    private void BandBuffer()
    {
        for (int i = 0; i < FREQ_BANDS_LENGTH; i++)
        {
            if (freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = min;
            }
            else if (freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= delta;
            }
        }
    }


    public static float[] GetBandBuffer()
    {
        return bandBuffer;
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
