using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionAbility : MainManager
{
    [SerializeField]
    float smLivetime;
    [SerializeField]
    float smMusicCoef;
    [SerializeField]
    float smMoveStep;

    private float startUseTime;
    private bool isAbilityActive;

    private void Update()
    {
        if (isAbilityActive)
        {
            if (GAME_TIME - startUseTime > smLivetime)
            {
                DestroySlowMotion();
            }
            else
            {
                ProgressSlowMotion();
            }
        }
    }

    public void CreateSlowMotion()
    {
        if (!isAbilityActive)
        {
            startUseTime = GAME_TIME;
            isAbilityActive = true;
        }
    }

    private void ProgressSlowMotion()
    {
        AudioManager.aManager.CreateSlowMotionEffect();
    }

    private void DestroySlowMotion()
    {
        isAbilityActive = false;
    }

    private void Initialize()
    {
        isAbilityActive = false;
    }

    public float GetMusicSlowCoef()
    {
        return smMusicCoef;
    }

    public float GetMoveSlowStep()
    {
        return smMoveStep;
    }

    public bool IsAbilityActive()
    {
        return isAbilityActive;
    }

    //Singleton
    public static SlowMotionAbility ability = null;

    void Start()
    {
        if (ability == null)
        {
            ability = this;
        }
        else if (ability == this)
        {
            Destroy(gameObject);
        }
        Initialize();
    }
}
