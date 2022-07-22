using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;

namespace TextFileContentAnalyzer.GUI.Services;

public interface IWordOccurrenceCounterFactory 
{
    IWordOccurrenceCounter Get();
}
