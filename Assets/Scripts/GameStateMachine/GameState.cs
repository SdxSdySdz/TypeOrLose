using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Race Race;
    
    public void Enter(Race race)
    {
        Race = race;
        
        if (enabled == false)
        {
            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(race);
            }
        }

        OnEnter();
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }

        OnExit();
    }
    
    public GameState GetNext()
    {
        foreach (var transition in _transitions)
        {
            if (transition.IsPossible)
            {
                return transition.NextState;
            }
        }

        return null;
    }

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
}
