using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurence;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;

public interface IWordOccurrenceCounter
{
    int TrackOccurances(string word);
    int Track(ReadOnlySpan<char> word);

    int EntriesCount { get; }

    IOrderedEnumerable<WordBucket> EnumerateAscending();
    IOrderedEnumerable<WordBucket> EnumerateDescending();

    int GetOccuranceCountForWord(string word);

}

