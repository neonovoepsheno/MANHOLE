using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyCollisionDetector : BallBehaviour
{
    [SerializeField]
    private GameObject plusOne;
    [SerializeField]
    private int yeGivenPoints;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            TouchPlayer();
        }
        else if (coll.gameObject.tag == "Heart")
        {
            TouchHeart();
        }
    }


    void TouchPlayer()
    {
        Instantiate(plusOne, transform.position, Quaternion.identity);
        IncrementingPlayerPoints(yeGivenPoints);
        IncrementingPlayerCombo(1);
        GUIScript.gui.UpdateGamePointsVisual();
        GUIScript.gui.UpdateComboVisual();
        GUIScript.gui.ChangeSpiralBarValue(-1 * GUIScript.gui.GetComboDelta());
        Destroy(transform.parent.gameObject);
    }


    void TouchHeart()
    {
        if (IsDeathMode())
        {
            GUIScript.gui.ShowLoseWindow();
            isLose = true;
            AudioManager.IsPause = true;
        }
        playerCombo = 0;
        GUIScript.gui.UpdateComboVisual();
        GUIScript.gui.SetSpiralBarValue(0);
        Destroy(transform.parent.gameObject);
    }
}
