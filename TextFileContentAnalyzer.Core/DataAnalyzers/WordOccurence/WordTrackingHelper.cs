﻿using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;
using TextFileContentAnalyzer.Core.Extensions;
using TextFileContentAnalyzer.Core.Util;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurence;

internal static class WordTrackingHelper
{
    /// <summary>
    /// Tracks occurrence of words from a string.
    /// </summary>
    /// <param name="text">The text to split into words based on white space characters.</param>
    /// <param name="counter">The counter where to track the words.</param>
    /// <param name="ct">A cancellation token to interrupt the action.</param>
    public static void TrackWords(string text, IWordOccurrenceCounter counter, CancellationToken ct)
    {
        foreach (var word in text.AsSpan().Split(ANSIIHelpers.WhiteSpaceCharacters, ignoreEmpty: true))
        {
            ct.ThrowIfCancellationRequested();
            counter.Track(word.Data);
        }
    }
}
