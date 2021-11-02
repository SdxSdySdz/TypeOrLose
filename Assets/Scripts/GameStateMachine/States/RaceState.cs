using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RaceState : GameState
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private WordTable _wordTablePrefab;
    [SerializeField] private List<string> _raceWords;

    private WordTable _wordTable;
    private WordsDictionary _wordsDictionary;

    protected override void OnEnter()
    {
        _camera.Follow = Race.MyRunner.gameObject.transform;
        _wordsDictionary = new WordsDictionary(_raceWords);
        
        CreateWordTable();
        UpdateWord();
    }

    protected override void OnExit()
    {
        _wordTable.LetterFilled.RemoveListener(OnLetterFilled);
        _wordTable.WordFilled.RemoveListener(OnWordFilled);
        Destroy(_wordTable);
    }

    private void CreateWordTable()
    {
        _wordTable = Instantiate(_wordTablePrefab, Vector3.zero, Quaternion.identity);
        _wordTable.Follow(Race.MyRunner);
        _wordTable.LetterFilled.AddListener(OnLetterFilled);
        _wordTable.WordFilled.AddListener(OnWordFilled);
    }
    
    private void OnLetterFilled()
    {
        Race.MyRunner.MakeStep();
    }
    
    private void OnWordFilled()
    {
        UpdateWord();
    }

    private void UpdateWord()
    {
        Word nextWord = _wordsDictionary.GetRandomWord();
        _wordTable.Init(nextWord);
    }
}
