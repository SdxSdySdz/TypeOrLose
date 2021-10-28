using System.Collections.Generic;
using System.Linq;
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
                transition.enabled = false;

            enabled = false;
        }

        OnExit();
    }
    
    public GameState GetNext()
    {
        return (from transition in _transitions where transition.IsPossible select transition.NextState).FirstOrDefault();
    }

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
}
