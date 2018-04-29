using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGeneration : ObjectGeneration
{
    [SerializeField]
    private GameObject[] abilityPrefabArray;
    [SerializeField]
    private float aStartGenerationDelay;
    [SerializeField]
    private float aGenerationDelay;
    [SerializeField]
    private float aGenerationChance;

    private float lastAbilityGeneration = 0f;


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable && GAME_TIME > aStartGenerationDelay)
        {
            if (IsGenerationTimeCondition(lastAbilityGeneration, aGenerationDelay))
            {
                if (IsAudioSignalCondition())
                {
                    if (IsGenerationChanceCondition(aGenerationChance))
                    {
                        GenerateAbility();
                    }
                    else
                    {
                        Debug.Log("Unlucky ability");
                        lastAbilityGeneration = GAME_TIME;
                    }
                }
            }
        }
    }


    private void GenerateAbility()
    {
        Vector3 generationPosition = (Random.insideUnitCircle * arenaRadius) + (Vector2)arena.transform.position;
        GameObject ability = Instantiate(abilityPrefabArray[(int)Random.Range(0f, abilityPrefabArray.Length)], generationPosition, Quaternion.identity);
        lastAbilityGeneration = GAME_TIME;
        DestroiyObjectOnCollision(ability, "Ability", generationPosition);
    }
}
