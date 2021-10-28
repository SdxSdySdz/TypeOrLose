using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private Race _race;
    [SerializeField] private GameState _firstState;

    private GameState _currentState;
    
    public GameState Current => _currentState;

    private void Awake()
    {
        foreach (var state in GetComponents<GameState>())
        {
            state.enabled = false;
        }

        foreach (var transition in GetComponents<Transition>())
        {
            transition.enabled = false;
        }
    }

    private void Start()
    {
        Clear(_firstState);
    }

    private void Update()
    {
        if (_currentState == null) return;

        var nextState = _currentState.GetNext();
        if (nextState != null)
        {
            MakeTransition(nextState);
        }
    }

    private void Clear(GameState state)
    {
        _currentState = state;

        if (_currentState != null)
        {
            _currentState.Enter(_race);
        }
    }

    private void MakeTransition(GameState nextState)
    {
        Debug.LogError($"Make transition {_currentState} {nextState}");
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = nextState;
        if (_currentState != null)
        {
            _currentState.Enter(_race);
        }
    }
}
