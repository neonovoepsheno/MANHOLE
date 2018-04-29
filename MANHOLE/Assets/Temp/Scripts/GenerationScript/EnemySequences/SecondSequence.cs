using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSequence : Sequence
{
    public static SecondSequence seq = null;

    void Start()
    {
        if (seq == null)
        {
            seq = this;
            Initialize();
        }
        else if (seq == this)
        {
            Destroy(gameObject);
        }
    }

    void Initialize()
    {

    }
}
