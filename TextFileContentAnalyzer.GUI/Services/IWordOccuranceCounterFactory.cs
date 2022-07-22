using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;

namespace TextFileContentAnalyzer.GUI.Services;

public interface IWordOccuranceCounterFactory 
{
    IWordOccuranceCounter Get();
}
