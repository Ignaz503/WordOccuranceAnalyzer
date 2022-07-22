using System.Collections.Concurrent;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurence;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;
public class ConcurrentDictionaryWordOccurrenceCounter : WordOccurrenceCounterBase
{
    ConcurrentDictionary<string, WordBucket> _entries = new();

    public override int EntriesCount => _entries.Count;

    protected override IEnumerable<WordBucket> Entries => _entries.Values;

    public override int GetOccuranceCountForWord(string word)
    {
        if (_entries.TryGetValue(word, out var newEntry))
            return newEntry?.Count ?? 0;
        return 0;
    }

    public override int TrackOccurances(string word)
    {
        int occ = 1;
        _entries.AddOrUpdate(word, k => new WordBucket(word), (key, value) => { occ = value.Increment(); return value; });
        return occ;
    }
}
