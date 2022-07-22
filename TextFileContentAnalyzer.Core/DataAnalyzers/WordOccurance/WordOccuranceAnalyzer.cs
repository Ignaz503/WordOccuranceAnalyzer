using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.ExecutionContexts;
using TextFileContentAnalyzer.Core.Optional;

namespace TextFileContentAnalyzer.Core.DataAnalyzer;

/// <summary>
/// Analyze a data stream on the occurance of words within it.
/// </summary>
public class WordOccuranceAnalyzer : IDataAnalyzer<WordOccuranceCounterExecutionContext>
{
    public Result<Okay,Exception> Analyze(WordOccuranceCounterExecutionContext ctx)
    {
        try
        {
            using var reader = new StreamReader(ctx.Stream);

            string? line;
            while ((line = reader.ReadLine()) != null) 
            {
                ctx.CancellationToken.ThrowIfCancellationRequested();
                ctx.ProgressPublisher.Publish(reader.BaseStream.Position);
                WordTrackingHelper.TrackWords(line, ctx.OccuranceCounter, ctx.CancellationToken);
            }
            ctx.ProgressPublisher.Publish(reader.BaseStream.Position);
            return new Okay().Some<Okay, Exception>();
        }
        catch (Exception ex)
        {
            return ex.From<Okay, Exception>();
        }
    }
}

