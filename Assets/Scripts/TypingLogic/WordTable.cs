using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WordFiller))]
[RequireComponent(typeof(InputTrigger))]
[RequireComponent(typeof(TypingSoundPlayer))]
public class WordTable : MonoBehaviour
{
    private WordFiller _wordFiller;
    private InputTrigger _inputTrigger;
    private readonly float _forwardOffset = 8;
    private readonly float _upOffset = 5;
    
    public UnityEvent LetterFilled;
    public UnityEvent WordFilled;
    
    private void Awake()
    {
        _wordFiller = GetComponent<WordFiller>();
        _inputTrigger = GetComponent<InputTrigger>();
    }

    private void OnEnable()
    {
        _inputTrigger.Triggered += OnCorrectUserInput;
    }

    private void OnDisable()
    {
        _inputTrigger.Triggered -= OnCorrectUserInput;
    }

    public void Init(Word word)
    {
        _wordFiller.SetWord(word);
        _inputTrigger.FollowLetter(_wordFiller.FirstUnfilledLetter);
    }
    
    public void Follow(Runner runner)
    {
        Transform target = runner.transform;
        
        Vector3 offset = target.forward * _forwardOffset + target.up * _upOffset;
        transform.position = target.position + offset;
        transform.LookAt(target);
        
        transform.SetParent(target);
    }

    private void OnCorrectUserInput()
    {
        if (_wordFiller.IsEmpty || _wordFiller.IsFilled) 
            return;
        
        LetterFilled?.Invoke();
        _wordFiller.FillNextLetter();
        
        if (_wordFiller.IsFilled)
        {
            _wordFiller.Clear();
            WordFilled?.Invoke();
        }
        else
            _inputTrigger.FollowLetter(_wordFiller.FirstUnfilledLetter);
    }
}
