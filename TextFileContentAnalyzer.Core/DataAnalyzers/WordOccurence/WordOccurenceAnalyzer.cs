using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurence;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.ExecutionContexts;
using TextFileContentAnalyzer.Core.Optional;

namespace TextFileContentAnalyzer.Core.DataAnalyzer.WordOccurrence;

/// <summary>
/// Analyze a data stream on the occurrence of words within it.
/// </summary>
public class WordOccurrenceAnalyzer : IDataAnalyzer<WordOccurrenceCounterExecutionContext>
{
    public Result<Okay,Exception> Analyze(WordOccurrenceCounterExecutionContext ctx)
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

