using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MainManager
{
    [SerializeField]
    private float cLivetime;

    private float creationTime;

    private void Start()
    {
        creationTime = GAME_TIME;
    }


    private void Update()
    {
        if (!GUIScript.isGUIWindowEnable && GAME_TIME - creationTime > cLivetime)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            IncrementingPlayerPoints();
            GUIScript.UpdateGamePointsVisual();
            GUIScript.ChangeSpiralBarValue(-1);
        }
    }
}
