using TextFileContentAnalyzer.Testing.Core;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.Testing.Core.Asserts;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;

namespace TextFileContentAnalyzer.Tests.Tests;

[Test]
public class DictionaryWordOccuranceCounterTests 
{

    [Fact]
    public void WordOccuranceCountShould_ContainWord_WhenTracked()
    {
        const string word = "test";
        var expectedCount = 1;
        var wordCounter = new DictionaryWordOccurrenceCounter();

        wordCounter.TrackOccurances(word);

        Assert.IsTrue(wordCounter.GetOccuranceCountForWord(word) == expectedCount);
    }

    [Fact]
    public void WordOccuranceCountShould_BeZero_WhenWordUntracked()
    {
        const string word = "test";
        var expectedCount = 0;
        var wordCounter = new DictionaryWordOccurrenceCounter();


        Assert.IsTrue(wordCounter.GetOccuranceCountForWord(word) == expectedCount);
    }

    [Fact]
    public void WordOccuranceCountShould_BeOneHigher_WhenTrackOccurancesCalledMultipleTimes() 
    {
        const string word = "test";
        var expectedCount = 2;
        var wordCounter = new DictionaryWordOccurrenceCounter();

        wordCounter.TrackOccurances(word);


        wordCounter.TrackOccurances(word);

        Assert.IsTrue(wordCounter.GetOccuranceCountForWord(word) == expectedCount);
    }

    [Fact]
    public void WordOccuranceCountShould_EnuerateInDescendingOrder_WhenEnumerateDescendingCalled()
    {
        const string word = "test";
        const string word2 = "test1";
        const string word3 = "test3";
        var wordCounter = new DictionaryWordOccurrenceCounter();

        wordCounter.TrackOccurances(word);
        wordCounter.TrackOccurances(word);

        wordCounter.TrackOccurances(word2);
        wordCounter.TrackOccurances(word2);
        wordCounter.TrackOccurances(word2);

        wordCounter.TrackOccurances(word3);

        var prevCount = int.MaxValue;

        foreach (var bucket in wordCounter.EnumerateDescending()) 
        {
            Assert.IsTrue(bucket.Count <= prevCount);
            prevCount = bucket.Count;
        }
    }
    [Fact]
    public void WordOccuranceCountShould_EnuerateInAscendingOrder_WhenEnumerateAscendingCalled()
    {
        const string word = "test";
        const string word2 = "test1";
        const string word3 = "test3";
        var wordCounter = new DictionaryWordOccurrenceCounter();

        wordCounter.TrackOccurances(word);
        wordCounter.TrackOccurances(word);

        wordCounter.TrackOccurances(word2);
        wordCounter.TrackOccurances(word2);
        wordCounter.TrackOccurances(word2);

        wordCounter.TrackOccurances(word3);

        var prevCount = int.MinValue;

        foreach (var bucket in wordCounter.EnumerateAscending())
        {
            Assert.IsTrue(bucket.Count >= prevCount);
            prevCount = bucket.Count;
        }
    }
}
