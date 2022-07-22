using TextFileContentAnalyzer.Testing.Core;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.Testing.Core.Asserts;

using System.Security.Cryptography;
using TextFileContentAnalyzer.Core.Optional;
using TextFileContentAnalyzer.Core.Util;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.ExecutionContexts;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;

namespace TextFileContentAnalyzer.Tests.Tests;

[Test]
public class AsyncWordOccuranceAnalyzerTests : IDisposable
{
    class MockedProgressReport : IAsyncProgressReport<long>
    {
        public async Task Report(long value)
        {
            await Task.Yield();
        }
    }

    CancellationTokenSource tokenSource;
    public AsyncWordOccuranceAnalyzerTests()
    {
        tokenSource = new CancellationTokenSource();
    }

    AsyncWordOccuranceCounterExecutionContext CreateExecutionContext(Stream s, IAsyncProgressReport<long> prog)
    {
        return new( prog, new DictionaryWordOccuranceCounter(),s ,tokenSource.Token );
    }

    [Fact]
    public async Task EmptyStringShould_HaveNoWordOccurances()
    {
        const string testString = "";
        var analzyer = new AsyncWordOccuranceAnalyzer();
        var prog = new MockedProgressReport();
        using var memStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(testString));
        var ctx = CreateExecutionContext(memStream, prog);
        var res = await analzyer.Analyze(ctx);

        Assert.IsTrue(res.HasValue);

        Assert.IsTrue(ctx.OccuranceCounter.EntriesCount == 0);
    }


    [Fact]
    public async Task SplittingOn_SpaceBarWhiteSpace_HasCorrectNumberOfEntries()
    {
        const int expetedCount = 4;
        const string testString = "These are my words";
        var analzyer = new AsyncWordOccuranceAnalyzer();
        var prog = new MockedProgressReport();
        using var memStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(testString));
        var ctx = CreateExecutionContext(memStream, prog);
        var res = await analzyer.Analyze(ctx);

        Assert.IsTrue(res.HasValue);
        var val = res.Unwrap()!;

        Assert.IsTrue(ctx.OccuranceCounter.EntriesCount == expetedCount, message:$"expeted {expetedCount} got {ctx.OccuranceCounter}");
    }

    [Fact]
    public async Task SplittingOn_ArbitraryAnsiiWhiteSpaceChars_HasCorrectNumberOfEntries()
    {
        const int expetedCount = 10;
        const string testString = "This enumerates all words\tafter tab\nafter newline \rafter return\vafter vtab \fafter feed";
        var analzyer = new AsyncWordOccuranceAnalyzer();
        var prog = new MockedProgressReport();
        using var memStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(testString));
        var ctx = CreateExecutionContext(memStream, prog);
        var res = await analzyer.Analyze(ctx);

        Assert.IsTrue(res.HasValue);
        var val = res.Unwrap()!;

        Assert.IsTrue(ctx.OccuranceCounter.EntriesCount == expetedCount, message: $"expeted {expetedCount} got {ctx.OccuranceCounter.EntriesCount}");
    }

    [Fact]
    public async Task WordOccuranceCount_Is_Correct()
    {
        const int expetedCount = 5;
        const string testString = "This enumerates all words\tafter tab\nafter newline \rafter return\vafter vtab \fafter feed";
        var analzyer = new AsyncWordOccuranceAnalyzer();
        var prog = new MockedProgressReport();
        using var memStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(testString));
        var ctx = CreateExecutionContext(memStream, prog);
        var res = await analzyer.Analyze(ctx);

        Assert.IsTrue(res.HasValue);
        var val = res.Unwrap()!;
        var count = ctx.OccuranceCounter.GetOccuranceCountForWord("after");
        Assert.IsTrue(count == expetedCount, message: $"expeted {expetedCount} got {count}");
    }

    [Fact]
    public async Task Output_Matches_Expectaion()
    {
        const string testString = @"1:1 Adam Seth Enos
1:2 Cainan Adam Seth Iared
";
        (string w, int c)[] words = new (string, int)[]
        {
            ("Adam",2),
            ("Seth",2),
            ("1:1",1),
            ("Enos",1),
            ("1:2",1),
            ("Cainan",1),
            ("Iared",1)
        };


        var analzyer = new AsyncWordOccuranceAnalyzer();
        var prog = new MockedProgressReport();
        using var memStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(testString));
        var ctx = CreateExecutionContext(memStream, prog);
        var res = await analzyer.Analyze(ctx);

        Assert.IsTrue(res.HasValue);
        foreach (var entry in words) 
        {
            var c = ctx.OccuranceCounter.GetOccuranceCountForWord(entry.w);
            Assert.IsTrue(c == entry.c, message: $"Expected{entry.c} got {c}");
        }

    }

    [Fact]
    public async Task FileAnalyzerShouldRun_IfWorkingOnRandomBytes()
    {
        var analzyer = new AsyncWordOccuranceAnalyzer();
        var prog = new MockedProgressReport();
        using var memStream = new MemoryStream(GenrateRandomData(4096));
        var ctx = CreateExecutionContext(memStream, prog);
        var res = await analzyer.Analyze(ctx);
    }


    [Fact]
    public async Task FileAnalyzerShouldThrow_IfCanceled()
    {
        var analzyer = new AsyncWordOccuranceAnalyzer();
        var prog = new MockedProgressReport();
        using var memStream = new MemoryStream(GenrateRandomData(1024));
        var ctx = CreateExecutionContext(memStream, prog);
        var deleayedExecution = new DelayedExecution<Result<Okay, Exception>>(
                () => analzyer.Analyze(ctx)
            );

        var task = deleayedExecution.RunDelayed();
        tokenSource.Cancel();
        var res = await task;

        Assert.IsFalse(res.HasValue);
        Assert.IsTrue(res.Exception?.GetType() == typeof(TaskCanceledException) || res.Exception?.GetType() == typeof(OperationCanceledException), message: $"got: {res.Exception?.GetType()}");
    }

    byte[] GenrateRandomData(int size) => RandomNumberGenerator.GetBytes(size);

    public void Dispose()
    {
        tokenSource.Cancel();
        tokenSource.Dispose();
    }

    class DelayedExecution<T> 
    {
        Func<Task<T>> toDelay;
        int amountInMS;

        public DelayedExecution(Func<Task<T>> toDelay, int amountInMS = 1000)
        {
            this.toDelay = toDelay;
            this.amountInMS = amountInMS;
        }

        public async Task<T> RunDelayed() 
        {
            await Task.Delay(amountInMS);
            var res =  await toDelay();
            return res;
        }

    }


}