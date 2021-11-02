using System;
using System.Collections.Generic;
using System.Linq;

public class WordsDictionary
{
    private readonly Random _random;
    private List<Word> _words;
    private int _wordIndex;
    
    public WordsDictionary(IEnumerable<Word> words)
    {
        _random = new Random();
        _words = new List<Word>(words);
        _wordIndex = 0;
    }

    public WordsDictionary(IEnumerable<string> words) : this(words.Select(word => new Word(word))) {  }

    public Word GetRandomWord()
    {
        // return GetWord(_wordIndex++ % _words.Count);
        int randomIndex = _random.Next(_words.Count);
        return GetWord(randomIndex);
    }

    public Word GetWord(int index)
    {
        return _words[index];
    }
}
