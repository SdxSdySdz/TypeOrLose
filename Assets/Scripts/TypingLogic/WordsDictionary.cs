using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class WordsDictionary
{
    private List<Word> _words;
    private int _wordIndex;
    
    public WordsDictionary(IEnumerable<Word> words)
    {
        _words = new List<Word>(words);
        _wordIndex = 0;
    }

    public WordsDictionary(IEnumerable<string> words) : this(words.Select(word => new Word(word))) {  }

    public Word GetRandomWord()
    {
        // return GetWord(_wordIndex++ % _words.Count);
        
        int randomIndex = Random.Range(0, _words.Count);
        return GetWord(randomIndex);
    }

    public Word GetWord(int index)
    {
        return _words[index];
    }
}
