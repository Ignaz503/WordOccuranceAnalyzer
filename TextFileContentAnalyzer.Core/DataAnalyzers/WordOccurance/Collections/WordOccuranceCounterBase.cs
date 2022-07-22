using TextFileContentAnalyzer.Core.DataAnalyzer;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;

public abstract class WordOccuranceCounterBase : IWordOccuranceCounter
{
    protected abstract IEnumerable<WordBucket> Entries { get; }


    public abstract int EntriesCount { get; }

    public IOrderedEnumerable<WordBucket> EnumerateAscending()
        => Entries.OrderBy(b => b.Count);
    public IOrderedEnumerable<WordBucket> EnumerateDescending()
        => Entries.OrderByDescending(b => b.Count);
    public abstract int GetOccuranceCountForWord(string word);
    public int Track(ReadOnlySpan<char> word)
        => TrackOccurances(new(word));
    public abstract int TrackOccurances(string word);
}
