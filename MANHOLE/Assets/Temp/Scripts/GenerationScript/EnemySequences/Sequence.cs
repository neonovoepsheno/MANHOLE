using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence :  MainManager
{
    [System.Serializable]
    public struct Enemy
    {
        public GameObject prefab;
        public int generationPoint;
        public float generationDelay;
    }

    [SerializeField]
    protected float seqGenerationChance;

    [SerializeField]
    protected Enemy[] Enemies;

    public float GetChance()
    {
        return seqGenerationChance;
    }

    
    public Enemy[] GetEnemies()
    {
        return Enemies;
    }
}
