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
    private float eGenerationDelay;

    private float lastEnemyGeneration = 0f;
    private int currentEnemyGenerationPoint = 0;


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable && GAME_TIME - startTime > TimeControlManager.startDelay)
        {
            CheckEnemyGenerationCondition();
        }
    }

    private void CheckEnemyGenerationCondition()
    {
        if (GAME_TIME - lastEnemyGeneration > eGenerationDelay)
        {

            Instantiate(enemyPrefabArray[(int)Random.Range(0f, enemyPrefabArray.Length - 1)], points[currentEnemyGenerationPoint].transform.position, Quaternion.identity);
            lastEnemyGeneration = GAME_TIME;
            currentEnemyGenerationPoint = (currentEnemyGenerationPoint + 1) % points.Length;
            return;
        }
    }
}
