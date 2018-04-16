using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MainManager
{
    public static AudioSource audioSource;
    public static bool IsPause
    {
        get
        {
            return _isPause;
        }
        set
        {
            _isPause = value;
            if (value)
            {
                pausedTimeSamples = audioSource.timeSamples;
            }
            else
            {
                PlayAudioWithTimeSamples(pausedTimeSamples);
            }
        }
    }

    static bool _isPause;

    [SerializeField]
    private float aUsualPitch;
    [SerializeField]
    private float aMinPitch;
    [SerializeField]
    private float aTimeToDecrease;

    static int pausedTimeSamples = 0;

    void Start()
    {
        SetInitValues();
    }


    void Update()
    {
        ControlPitch();
    }


    void SetInitValues()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = aUsualPitch;
        IsPause = false;
        audioSource.Stop();
    }


    void ControlPitch()
    {
        //Debug.Log(audioSource.timeSamples);
        if (_isPause)
        {
            if (audioSource.pitch > aMinPitch)
            {
                audioSource.pitch -= Time.deltaTime * aUsualPitch * aTimeToDecrease;
               
                //audioSource.timeSamples = audioSource.clip.samples - 1;
            }
            if (audioSource.timeSamples == 0)
            {
                PlayAudioWithTimeSamples(audioSource.clip.samples - 1);
            }
        }
        else
        {
            if (audioSource.pitch < aUsualPitch)
            {
                audioSource.pitch += Time.deltaTime * aUsualPitch * aTimeToDecrease;
            }
        }
    }


    static void PlayAudioWithTimeSamples(int timeSamples)
    {
        audioSource.Stop();
        audioSource.timeSamples = timeSamples;
        audioSource.Play();
    }
}
