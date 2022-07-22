using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurence;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;

public abstract class WordOccurrenceCounterBase : IWordOccurrenceCounter
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
