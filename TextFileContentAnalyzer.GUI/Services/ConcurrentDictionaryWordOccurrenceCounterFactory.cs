using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;

namespace TextFileContentAnalyzer.GUI.Services;

public class ConcurrentDictionaryWordOccurrenceCounterFactory : IWordOccurrenceCounterFactory
{
    public IWordOccurrenceCounter Get()
        => new ConcurrentDictionaryWordOccurrenceCounter();
}