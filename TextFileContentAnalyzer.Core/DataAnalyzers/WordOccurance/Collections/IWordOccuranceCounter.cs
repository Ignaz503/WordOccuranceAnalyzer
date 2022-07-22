using TextFileContentAnalyzer.Core.DataAnalyzer;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;

/// <summary>
/// Defines a WordOccuranceCounter as neede by the WordOccuranceAnalyzer
/// </summary>
public interface IWordOccuranceCounter
{
    int TrackOccurances(string word);
    int Track(ReadOnlySpan<char> word);

    int EntriesCount { get; }

    IOrderedEnumerable<WordBucket> EnumerateAscending();
    IOrderedEnumerable<WordBucket> EnumerateDescending();

    int GetOccuranceCountForWord(string word);

}

