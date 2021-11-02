public class Word
{
    private string _word;

    public int Length => _word.Length;
    public string AsString => _word;
    
    public Word(string word)
    {
        _word = word;
    }

    public char GetLetter(int index)
    {
        return _word[index];
    }
}
