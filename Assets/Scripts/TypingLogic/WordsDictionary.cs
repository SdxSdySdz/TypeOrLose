using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class WordsDictionary
{
    private readonly List<Word> _words;

    public WordsDictionary(IEnumerable<Word> words)
    {
        _words = new List<Word>(words);
    }

    public WordsDictionary(IEnumerable<string> words) : this(words.Select(word => new Word(word))) {  }

    public Word this[int index]
    {
        get
        {
            if (index < 0 || index >= _words.Count)
                throw new IndexOutOfRangeException($"Words dictionary doesnt contain word with index {index}");
            
            return _words[index];
        }
    }

    public Word GetRandomWord()
    {
        int randomIndex = Random.Range(0, _words.Count);
        return _words[randomIndex];
    }
}
