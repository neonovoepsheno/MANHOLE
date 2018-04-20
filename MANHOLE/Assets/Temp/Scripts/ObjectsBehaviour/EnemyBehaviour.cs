using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : BallBehaviour
{
    private GameObject target;
    [SerializeField]
    private float eSpeed;
    [SerializeField]
    private float eInnerScale;
    [SerializeField]
    private float eChangeScaleRadius;

    private float startScale;
    private float[] eScaleCoefArray;

    private void Start()
    {
        SetInitValues();
    }


    void Update()
    {
        if (!GUIScript.isGUIWindowEnable)
        {
            SimpleEnemyMoving();
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            IncrementingPlayerPoints();
            GUIScript.UpdateGamePointsVisual();
        }
        else if (coll.gameObject.tag == "Heart")
        {
            //Debug.Log("You lose");

            GUIScript.ShowLoseWindow();
            isLose = true;
            Destroy(gameObject);
        }
    }


    void SetInitValues()
    {
        target = GameObject.FindGameObjectWithTag("Heart");

        startScale = transform.localScale.x;

        eScaleCoefArray = new float[2];
        LinearCoefSelection(startScale, eInnerScale, eChangeScaleRadius, 0f, eScaleCoefArray);
    }


    void SimpleEnemyMoving()
    {
        float step = eSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    //Dont need here
    void ChangeScaleWhileMoving()
    {
        float radius = Vector3.Magnitude(target.transform.position - transform.position);
        float currScale = startScale;
        if (radius < eChangeScaleRadius)
        {
            currScale = UpdateValueWithLinearCoef(eScaleCoefArray, radius);
        }
        transform.localScale = new Vector3(currScale, currScale, currScale);
    }
}
