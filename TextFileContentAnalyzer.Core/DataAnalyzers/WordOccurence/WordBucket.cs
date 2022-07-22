
using System.Diagnostics.CodeAnalysis;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurence;

/// <summary>
/// represents a word and it's occurrences in something.
/// </summary>
public class WordBucket : IEquatable<WordBucket>, IComparable, IComparable<WordBucket>
{

    public string Word { get; init; }

    private int count;
    public int Count => count;

    public WordBucket()
    {
        Word = string.Empty;
        count = 1;
    }

    public WordBucket(string word)
    {
        Word = word;
        count = 1;
    }


    public int Increment()
        => Interlocked.Increment(ref count);


    public bool Equals(WordBucket? other)
    {
        if (other is null)
            return false;
        return Word.Equals(other.Word) && Count.Equals(other.Count);
    }

    public int CompareTo(WordBucket? other)
    {
        if (other is null)
            return 1;
        return Count.CompareTo(other.Count);
    }

    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;
        if (obj is not WordBucket other)
            throw new ArgumentException("Must be word bucket to compare to");
        return CompareTo(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Word, count);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not WordBucket other)
            return false;
        return Equals(other);
    }

    public override string? ToString()
        => $"{Word}: {Count}";
}

