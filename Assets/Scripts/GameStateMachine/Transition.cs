using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private GameState _nextState;

    protected Race Race;
    
    public GameState NextState => _nextState;
    public bool IsPossible { get; protected set; }
    
    private void OnEnable()
    {
        IsPossible = false;
    }
    
    public void Init(Race race)
    {
        Race = race;
    }
}
