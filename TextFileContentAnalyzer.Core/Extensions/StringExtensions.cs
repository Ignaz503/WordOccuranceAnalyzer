using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileContentAnalyzer.Core.Extensions;

/// <summary>
/// String extensions that allow for splitting of strings without extra allocations
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Splits a character span based on the seperator character without allocation
    /// </summary>
    /// <param name="s">The character span</param>
    /// <param name="separator">The seperator char</param>
    /// <returns>An enumeration of all splits</returns>
    public static SplitEnumerator Split(this ReadOnlySpan<char> s, char separator)
    {
        if (s.IsEmpty)
        {
            return SplitEnumerator.Empty;
        }
        return new SplitEnumerator(s, separator);
    }

    /// <summary>
    /// Splits a character span based on any of the characters in the seperator span without allocation
    /// </summary>
    /// <param name="s">The character span to split</param>
    /// <param name="separator">The seperator span</param>
    /// <param name="ignoreEmpty">Informs if empty split results should be reported or not</param>
    /// <returns>An enumeration of all split entries</returns>
    public static SplitEnumeratorMultiSeperator Split(this ReadOnlySpan<char> s, ReadOnlySpan<char> separator, bool ignoreEmpty = true)
    {
        if (s.IsEmpty)
        {
            return SplitEnumeratorMultiSeperator.Empty;
        }
        return new SplitEnumeratorMultiSeperator(s, separator, ignoreEmpty);
    }
}
