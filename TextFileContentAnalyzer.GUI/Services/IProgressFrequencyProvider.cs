using System;

namespace TextFileContentAnalyzer.GUI.Services;

public interface IProgressFrequencyProvider 
{
    public TimeSpan Frequency { get; }
}
