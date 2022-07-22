namespace TextFileContentAnalyzer.Core.Extensions;

/// <summary>
/// Handles splitting of character spans based on single seperator characters
/// ref struct implementation of IEnumerator<SplitEntry> to make it useable with foreach
/// </summary>
public ref struct SplitEnumerator
{
    private ReadOnlySpan<char> _str;
    private readonly char seperator;

    public SplitEnumerator(ReadOnlySpan<char> str, char seperator)
    {
        _str = str;
        this.seperator = seperator;
        Current = default;
    }

    // Needed to be compatible with the foreach operator
    public SplitEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
        var span = _str;
        if (span.Length == 0) // Reach the end of the string
            return false;

        var index = span.IndexOf(seperator);
        if (index == -1)//no occurance
        {
            _str = ReadOnlySpan<char>.Empty;
            Current = new() { Data = span, ReadToPosition = Current.ReadToPosition + span.Length };
            return true;
        }

        if (index < span.Length - 1 && span[index] == seperator)
        {

            Current = new() { Data = span[..index], ReadToPosition = Current.ReadToPosition + index + 1 };

            _str = span[(index + 1)..];
            return true;

        }

        Current = new() { Data = span[..index], ReadToPosition = Current.ReadToPosition + index + 1};
        _str = span[(index + 1)..];
        return true;

    }

    public SplitEntry Current { get; private set; }

    public static SplitEnumerator Empty => new(ReadOnlySpan<char>.Empty, char.MinValue);

}