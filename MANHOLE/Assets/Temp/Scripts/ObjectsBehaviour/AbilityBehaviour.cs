using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBehaviour : MainManager
{
    [SerializeField]
    private float aLivetime;

    private float creationTime;
    private string abilityID;

    private void Start()
    {
        creationTime = GAME_TIME;
        abilityID = gameObject.tag;
    }


    private void Update()
    {
        if (!GUIScript.isGUIWindowEnable && GAME_TIME - creationTime > aLivetime)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("You use " + abilityID);
            GUIScript.ChangeSpiralBarValue(-1);
        }
    }
}
