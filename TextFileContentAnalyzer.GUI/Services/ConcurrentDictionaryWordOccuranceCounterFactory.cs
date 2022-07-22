using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;

namespace TextFileContentAnalyzer.GUI.Services;

public class ConcurrentDictionaryWordOccuranceCounterFactory : IWordOccuranceCounterFactory
{
    public IWordOccuranceCounter Get()
        => new ConcurrentDictionaryWordOccuranceCounter();
}