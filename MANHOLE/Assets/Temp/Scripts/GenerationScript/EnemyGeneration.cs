using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : ObjectGeneration
{

	[SerializeField]
	private GameObject[] points;

    float lastEnemyGeneration = 0f;
    float[] pitchRange = { 50, 60 };
    float currentPitch;

    Sequence currentSequence;
    int currentEnemy;
    bool isSequenceSelected;
    float selectChance;

    void Start()
    {
        Initialize();
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable && GAME_TIME - startTime > TimeControlManager.startDelay)
        {
            if (isSequenceSelected)
            {
                GenerateSelectedSequence();
            }
            else
            {
                SelectSequence();
            }
        }
    }


    void GenerateSelectedSequence()
    {
        if (GAME_TIME - lastEnemyGeneration > currentSequence.GetEnemies()[currentEnemy].generationDelay)
        {
            Instantiate(currentSequence.GetEnemies()[currentEnemy].prefab, points[currentSequence.GetEnemies()[currentEnemy].generationPoint].transform.position, Quaternion.identity);
            lastEnemyGeneration = GAME_TIME;
            currentEnemy++;
            if (currentEnemy == currentSequence.GetEnemies().Length)
            {
                SetSeqValueNull();
            }
        }
    }


    void SelectSequence()
    {
        currentPitch = AudioAnalysis.GetPitchValue();
        if (currentPitch >= pitchRange[0] && currentPitch <= pitchRange[1])
        {
            if (CheckFirstSelection())
            {
                return;
            }
            else if (CheckSecondSelection())
            {
                return;
            }
        }
    }


    bool CheckFirstSelection()
    {
        selectChance = Random.Range(0f, 1f);
        if (selectChance <= FirstSequence.seq.GetChance())
        {
            Debug.Log("we choose first seq");
            isSequenceSelected = true;
            currentSequence = FirstSequence.seq;
            return true;
        }
        return false;
    }


    bool CheckSecondSelection()
    {
        selectChance = Random.Range(0f, 1f);
        if (selectChance <= SecondSequence.seq.GetChance())
        {
            isSequenceSelected = true;
            currentSequence = SecondSequence.seq;
            return true;
        }
        return false;
    }


    void Initialize()
    {
        SetSeqValueNull();
        lastEnemyGeneration = 0;
    }


    void SetSeqValueNull()
    {
        currentSequence = null;
        currentEnemy = 0;
        isSequenceSelected = false;
    }
}
