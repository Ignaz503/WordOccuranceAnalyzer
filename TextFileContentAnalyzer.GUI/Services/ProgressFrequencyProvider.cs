using System;

namespace TextFileContentAnalyzer.GUI.Services;

public class ProgressFrequencyProvider : IProgressFrequencyProvider
{
    public TimeSpan Frequency { get; set; }
}
