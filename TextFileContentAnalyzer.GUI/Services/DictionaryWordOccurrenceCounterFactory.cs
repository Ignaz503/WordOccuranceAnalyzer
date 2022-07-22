using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;

namespace TextFileContentAnalyzer.GUI.Services;

public class DictionaryWordOccurrenceCounterFactory : IWordOccurrenceCounterFactory
{
    public IWordOccurrenceCounter Get() 
        => new DictionaryWordOccurrenceCounter();
}
