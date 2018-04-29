using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneration : ObjectGeneration
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private float cStartGenerationDelay;
    [SerializeField]
    private float cGenerationDelay;
    [SerializeField]
    private float cGenerationChance;

    private float lastCoinGeneration = 0f;


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable && GAME_TIME > cStartGenerationDelay)
        {
            if (IsGenerationTimeCondition(lastCoinGeneration, cGenerationDelay))
            {
                if (IsAudioSignalCondition())
                {
                    if (IsGenerationChanceCondition(cGenerationChance))
                    {
                        GenerateCoin();
                    }
                    else
                    {
                        Debug.Log("Unlucky coin");
                        lastCoinGeneration = GAME_TIME;
                    }
                }
            }
        }
    }


    private void GenerateCoin()
    {
        Vector3 generationPosition = (Random.insideUnitCircle * arenaRadius) + (Vector2)arena.transform.position;
        GameObject coin = Instantiate(coinPrefab, generationPosition, Quaternion.identity);
        lastCoinGeneration = GAME_TIME;
        DestroiyObjectOnCollision(coin, "Coin", generationPosition);
    }
}
