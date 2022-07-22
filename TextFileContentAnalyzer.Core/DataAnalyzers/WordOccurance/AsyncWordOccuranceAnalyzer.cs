using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.ExecutionContexts;
using TextFileContentAnalyzer.Core.Optional;

namespace TextFileContentAnalyzer.Core.DataAnalyzer;

/// <summary>
/// Analyze a data stream on the occurance of words within it
/// </summary>
public class AsyncWordOccuranceAnalyzer : IAsyncDataAnalyzer<AsyncWordOccuranceCounterExecutionContext>
{

    /// <summary>
    /// Analyze a data stream on word occurance.
    /// </summary>
    /// <param name="ctx">The execution context such as the stream, a cancelation context and a progress report handler.</param>
    /// <returns>Okay Result if everything worked fine, or a Result Exception if something went wrong.</returns>
    public async Task<Result<Okay, Exception>> Analyze(AsyncWordOccuranceCounterExecutionContext ctx)
    {
        try
        {
            using var reader = new StreamReader(ctx.Stream);

            while (!reader.EndOfStream) 
            {
                var line = await reader.ReadLineAsync();
                ctx.CancellationToken.ThrowIfCancellationRequested();
                if (line is null)
                    continue;
                await ctx.Progress.Report(reader.BaseStream.Position);
                WordTrackingHelper.TrackWords(line,ctx.OccuranceCounter, ctx.CancellationToken);

            }

            await ctx.Progress.Report(reader.BaseStream.Length);

            return new Okay().Some<Okay, Exception>();
        }
        catch (Exception ex) 
        {
            return ex.From<Okay, Exception>();
        }
    }
}

