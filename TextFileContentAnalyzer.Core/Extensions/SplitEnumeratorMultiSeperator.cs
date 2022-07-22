namespace TextFileContentAnalyzer.Core.Extensions;

/// <summary>
/// Handles splitting of character spans based on multiple seperator characters
/// ref struct implementation of IEnumerator<SplitEntry> to make it useable with foreach
/// </summary>
public ref struct SplitEnumeratorMultiSeperator
{
    private ReadOnlySpan<char> _str;
    private readonly ReadOnlySpan<char> seperators;
    private readonly bool ignoreEmtpy;

    public SplitEnumeratorMultiSeperator(ReadOnlySpan<char> str, ReadOnlySpan<char> seperators, bool ignoreEmpty)
    {
        _str = str;
        this.seperators = seperators;
        Current = default;
        this.ignoreEmtpy = ignoreEmpty;
    }

    // Needed to be compatible with the foreach operator
    public SplitEnumeratorMultiSeperator GetEnumerator() => this;

    public bool MoveNext()
    {
        var span = _str;
        if (span.Length == 0) // Reach the end of the string
            return false;

        var index = span.IndexOfAny(seperators);
        if (index == -1)  //no occurrence
        {
            _str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
            Current = new() { Data = span, ReadToPosition = Current.ReadToPosition + span.Length };
            return true;
        }

        if (index < span.Length - 1 && span[index] == seperators[0])
        {
            var nextIndexOffset = 1;
            var next = span[index + nextIndexOffset];
            //consume all seperators
            while (InIdxRange(nextIndexOffset, seperators.Length) && seperators[nextIndexOffset] == next)
            {
                nextIndexOffset++;
                next = span[index + nextIndexOffset];
            }
            if (!InIdxRange(nextIndexOffset, seperators.Length))
            {
                Current = new() { Data = span[..index], ReadToPosition = Current.ReadToPosition + index  + nextIndexOffset};

                _str = span[(index + nextIndexOffset)..];

                if (ignoreEmtpy && Current.Data.Length == 0)
                    MoveNext();
                return true;
            }
        }

        Current = new() { Data = span[..index], ReadToPosition = Current.ReadToPosition + index  + 1};
        _str = span[(index + 1)..];

        if (ignoreEmtpy && Current.Data.Length == 0)
            MoveNext();

        return true;

        bool InIdxRange(int idx, int length)
            => idx < length;

    }

    public SplitEntry Current { get; private set; }

    public static SplitEnumeratorMultiSeperator Empty => new(ReadOnlySpan<char>.Empty, ReadOnlySpan<char>.Empty, true);

}
