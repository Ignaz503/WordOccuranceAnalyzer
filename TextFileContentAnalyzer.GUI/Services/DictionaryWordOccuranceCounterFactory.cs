using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;

namespace TextFileContentAnalyzer.GUI.Services;

public class DictionaryWordOccuranceCounterFactory : IWordOccuranceCounterFactory
{
    public IWordOccuranceCounter Get() 
        => new DictionaryWordOccuranceCounter();
}
