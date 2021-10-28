using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyTransition : Transition
{
    private void Update()
    {
        if (Race.AreAllRunnersReady)
        {
            IsPossible = true;
        }
    }
}
