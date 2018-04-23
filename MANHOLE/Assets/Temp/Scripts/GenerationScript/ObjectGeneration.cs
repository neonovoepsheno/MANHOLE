using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MainManager
{
    [SerializeField]
    protected GameObject arena;
    [SerializeField]
    private int bufferBandN;
    [SerializeField]
    private float minBufferBandGenerationObj;

    protected float arenaRadius;

    private void Start()
    {
        arenaRadius = GetObjectRadius(arena) / 2;
    }


    protected bool IsGenerationTimeCondition(float lastGeneration, float generationDelay)
    {
        if (GAME_TIME - lastGeneration > generationDelay)
        {
            return true;
        }
        return false;
    }


    protected bool IsGenerationChanceCondition(float generationChance)
    {
        if (Random.value <= generationChance)
        {
            return true;
        }
        return false;
    }


    protected bool IsAudioSignalCondition()
    {
        float soundValue = AudioAnalysis.GetPitchValue();
        return soundValue >= minBufferBandGenerationObj;
    }


    protected float GetObjectRadius(GameObject gObject)
    {
        return gObject.GetComponent<CircleCollider2D>().radius * gObject.transform.localScale.x;
    }


    protected void DestroiyObjectOnCollision(GameObject gObject, string oTag, Vector3 generationPosition)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(generationPosition, GetObjectRadius(gObject));
        int i = 0;
        bool isFirstObjectCollider = true;
        while (i < hitColliders.Length)
        {
            string tag = hitColliders[i].gameObject.tag;
            if (tag == oTag)
            {
                if (isFirstObjectCollider)
                {
                    isFirstObjectCollider = false;
                }
                else
                {
                    Destroy(gObject);
                    return;
                }
            }
            else if (tag != "Arena" && tag != "Player" && tag != "Enemy" && tag != "Untagged")
            {
                Destroy(gObject);
                return;
            }
            i++;
        }
    }
}
