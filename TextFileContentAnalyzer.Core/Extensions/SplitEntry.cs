namespace TextFileContentAnalyzer.Core.Extensions;

/// <summary>
/// represents a split from a character span
/// </summary>
public ref struct SplitEntry 
{
    public ReadOnlySpan<char> Data { get; init; }
    public long ReadToPosition { get; set; }
}
