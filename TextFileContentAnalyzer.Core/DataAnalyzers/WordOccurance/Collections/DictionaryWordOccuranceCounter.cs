using TextFileContentAnalyzer.Core.DataAnalyzer;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;



/// <summary>
/// Word Occurance counter that stores entries in a dictionary
/// </summary>
public class DictionaryWordOccuranceCounter : WordOccuranceCounterBase
{
    readonly Dictionary<string, WordBucket> _entries;

    public override int EntriesCount => _entries.Count;

    protected override IEnumerable<WordBucket> Entries => _entries.Values;

    public DictionaryWordOccuranceCounter() : base()
    {
        _entries = new();
    }
    public override int TrackOccurances(string word)
    {
        if (_entries.ContainsKey(word))
        {
            return _entries[word].Increment();

        }
        var newEntry = new WordBucket() { Word = word };
        _entries.Add(word, newEntry);
        return 1;
    }

    public override int GetOccuranceCountForWord(string word)
    {
        if (_entries.ContainsKey(word))
            return _entries[word].Count;
        return 0;
    }
}
