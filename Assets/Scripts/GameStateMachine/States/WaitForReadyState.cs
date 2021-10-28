using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForReadyState : GameState
{
    [SerializeField] private WordTable _wordTablePrefab;

    private WordTable _wordTable;
    
    private void Start()
    {
    }

    protected override void OnEnter()
    {
        _wordTable = Instantiate(_wordTablePrefab, Vector3.zero, Quaternion.identity);
        Debug.Log($"My runner {Race.MyRunner}");

        _wordTable.WordFilled.AddListener(OnMyRunnerReady);
        _wordTable.Follow(Race.MyRunner);
        _wordTable.Init(new Word("Ready"));
    }

    protected override void OnExit()
    {
        _wordTable.WordFilled.RemoveListener(OnMyRunnerReady);
        Destroy(_wordTable.gameObject);
        Debug.LogError("Table destroyed");
    }

    private void OnMyRunnerReady()
    {
        Race.MarkMyRunnerAsReady();
    }
}
