using UnityEngine;
using UnityEngine.UI;

public class WordFiller : MonoBehaviour
{
    [SerializeField] private Color _correctLetterColor;
    [SerializeField] private Color _incorrectLetterColor;

    private Word _word;
    private Text _text;
    private int _filledLettersCount;

    public char FirstUnfilledLetter => _word[_filledLettersCount];
    public bool IsEmpty => _word == null;
    public bool IsFilled => _filledLettersCount >= _word.Length;

    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
    }

    public void SetWord(Word word)
    {
        _filledLettersCount = 0;
        _word = word;
        FillLetters(_incorrectLetterColor);
    }
    
    public void FillNextLetter()
    {
        _filledLettersCount++;
        FillLetters(0, _filledLettersCount - 1, _correctLetterColor);
    }
    
    public void Clear()
    {
        _filledLettersCount = 0;
        _word = null;
        _text.text = "";
    }
    
    private void FillLetters(Color color)
    {
        FillLetters(0, _word.Length - 1, color);
    }

    private void FillLetters(int startIndex, int endIndex, Color color)
    {
        string newText = _word.AsString;
        
        newText = newText.Insert(endIndex + 1, $"</color>");
        newText = newText.Insert(startIndex, $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>");
        
        _text.text = newText;
    }
}