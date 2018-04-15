using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : ObjectGeneration
{

	[SerializeField]
	private GameObject[] points;
    [SerializeField]
    private GameObject[] enemyPrefabArray;
    [SerializeField]
    private float eStartGenerationDelay;
    [SerializeField]
    private float eGenerationDelay;

    private float lastEnemyGeneration = 0f;
    private int currentEnemyGenerationPoint = 0;


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable && GAME_TIME > eStartGenerationDelay)
        {
            CheckEnemyGenerationCondition();
        }
    }

    private void CheckEnemyGenerationCondition()
    {        
        if (GAME_TIME - lastEnemyGeneration > eGenerationDelay)
        {
            if (IsAudioSignalCondition())
            {
                Instantiate(enemyPrefabArray[(int)Random.Range(0f, enemyPrefabArray.Length - 1)], points[currentEnemyGenerationPoint].transform.position, Quaternion.identity);
                lastEnemyGeneration = GAME_TIME;
                currentEnemyGenerationPoint = (currentEnemyGenerationPoint + 1) % points.Length;
                return;
            }
        }
    }
}
