using UnityEngine;

public class WaitForReadyState : GameState
{
    [SerializeField] private WordTable _wordTablePrefab;

    private WordTable _wordTable;

    protected override void OnEnter()
    {
        _wordTable = Instantiate(_wordTablePrefab, Vector3.zero, Quaternion.identity);

        _wordTable.WordFilled.AddListener(Race.MyRunner.OnReady);
        _wordTable.Follow(Race.MyRunner);
        _wordTable.Init(new Word("Ready"));
    }

    protected override void OnExit()
    {
        _wordTable.WordFilled.RemoveListener(Race.MyRunner.OnReady);
        Destroy(_wordTable.gameObject);
    }
}
