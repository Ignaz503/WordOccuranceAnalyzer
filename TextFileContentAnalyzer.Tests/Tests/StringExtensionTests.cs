using TextFileContentAnalyzer.Testing.Core;
using TextFileContentAnalyzer.Core.Extensions;
using TextFileContentAnalyzer.Testing.Core.Asserts;

namespace TextFileContentAnalyzer.Tests.Tests;

[Test]
public class StringExtensionTests 
{
    [Fact]
    public void SplitingOnCharacterEnumeratesAllValues()
    {
        const string testStringFirst = "This is the test strings first part";
        const string testStringSecond = " this is the test strings second part";
        const string testString = testStringFirst + "," + testStringSecond;

        var entries = new List<string>();

        foreach (var entry in testString.AsSpan().Split(','))
            entries.Add(new(entry.Data));

        Assert.IsTrue(entries.Count == 2);
        Assert.IsTrue(entries[0] == testStringFirst);
        Assert.IsTrue(entries[1] == testStringSecond);

    }

    [Fact]
    public void SplitingOnMultipleCharacterEnumeratesAllValues()
    {
        const string testStringFirst = "Thisistheteststringsfirstpart";
        const string testStringSecond = " thisistheteststringssecondpart";
        const string testString = testStringFirst + "," + testStringSecond;

        var entries = new List<string>();
        var splitChars = ", ";
        foreach (var entry in testString.AsSpan().Split(splitChars))
            entries.Add(new(entry.Data));

        Assert.IsTrue(entries.Count == 2, message: $"expeted 2 got {entries.Count}");
        Assert.IsTrue(entries[0] == testStringFirst);
        Assert.IsTrue(entries[1] == testStringSecond.Trim());

    }
    [Fact]
    public void EnumeratingAllWordsSeperatedBySpaceBarWhiteSpace()
    {
        const int expectedCount = 4;
        const string testString = "This enumerates all words";
        var entries = new List<string>();
        var splitChars = " ";
        foreach (var entry in testString.AsSpan().Split(splitChars))
            entries.Add(new(entry.Data));

        Assert.IsTrue(entries.Count == expectedCount, message: $"Expeted {expectedCount} got {entries.Count}");

    }

    [Fact]
    public void EnumeratingAllWordsSeperatedByAnyWhiteSpace()
    {
        const int expectedCount = 14;
        const string testString = "This enumerates all words\tafter tab\nafter newline \rafter return\vafter vtab \fafter feed";
        var entries = new List<string>();
        var splitChars = " \n\t\r\v\f";
        foreach (var entry in testString.AsSpan().Split(splitChars))
            entries.Add(new(entry.Data));

        Assert.IsTrue(entries.Count == expectedCount, message: $"Expeted {expectedCount} got {entries.Count}");

    }
    [Fact]
    public void ReadToPositionsCorrect()
    {
        const string testString = "This enumerates all words\tafter tab\nafter newline \rafter return\vafter vtab \fafter feed";

        var splitChars = " \n\t\r\v\f";
        SplitEntry lastEntry = new();
        foreach (var entry in testString.AsSpan().Split(splitChars)) 
        {
            lastEntry = entry;
        }
        Assert.IsTrue(lastEntry.ReadToPosition == testString.Length, message: $"Expeted: {testString.Length} got: {lastEntry.ReadToPosition}");
        Assert.IsTrue(true);

    }
}