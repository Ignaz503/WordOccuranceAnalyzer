using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurence;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;

public class DictionaryWordOccurrenceCounter : WordOccurrenceCounterBase
{
    readonly Dictionary<string, WordBucket> _entries;

    public override int EntriesCount => _entries.Count;

    protected override IEnumerable<WordBucket> Entries => _entries.Values;

    public DictionaryWordOccurrenceCounter() : base()
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
