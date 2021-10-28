using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceIsRunTransition : Transition
{
    private void Update()
    {
        if (Race.IsRun)
            IsPossible = true;
    }
}
