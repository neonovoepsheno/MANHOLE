using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIComboVisual : MainManager
{
    [SerializeField]
    int guiMinComboVisual;
    [SerializeField]
    int[] guiComboVisualArray;

    int currComboVisual;
    Animator animator;
    Text text;

    public void InitVal()
    {
        animator = gameObject.GetComponent<Animator>();
        text = gameObject.GetComponent<Text>();
    }


    public int GetMin()
    {
        return guiMinComboVisual;
    }


    public void UpdateVisual()
    {
        bool playerComboCondition = (playerCombo >= guiMinComboVisual);
        gameObject.SetActive(playerComboCondition);
        text.text = "x" + GetCurrentVisualCombo();
    }


    int GetCurrentVisualCombo()
    {
        int temp_value = guiComboVisualArray[guiComboVisualArray.Length - 1];
        if (playerCombo > temp_value)
        {
            return temp_value;
        }
        for (int i = 0; i < guiComboVisualArray.Length; i++)
        {
            if (guiComboVisualArray[i] > playerCombo)
            {
                break;
            }
            temp_value = guiComboVisualArray[i];
        }
        if (temp_value != currComboVisual)
        {
            animator.SetBool("IsChanged", true);
        }
        currComboVisual = temp_value;
        return temp_value;
    }


    public void CheckComboAnimator()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ComboBounce"))
        {
            animator.SetBool("IsChanged", false);
        }
    }
}
