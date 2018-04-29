using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOneBehaviour : AbilityBehaviour
{
	[SerializeField]
	float poStep;

	Vector3 position;
    Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    void Update () {
        MovingUp();
        CheckDestroyCondition();
    }


    void MovingUp()
    {
        position = transform.position;
        transform.position = position + new Vector3(0, poStep * Time.deltaTime, 0);
    }


    void CheckDestroyCondition()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
