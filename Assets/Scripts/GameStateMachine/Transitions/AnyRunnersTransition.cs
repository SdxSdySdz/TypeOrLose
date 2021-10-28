using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyRunnersTransition : Transition
{
    private void Update()
    {
        if (Race.RunnersCount > 0)
            IsPossible = true;
        /*if (Race.MyRunner != null)
            IsPossible = true;*/
    }
}
