using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;
using TextFileContentAnalyzer.Core.Optional;
using TextFileContentAnalyzer.Core.Util;

namespace TextFileContentAnalyzer.GUI.Services.WordOccurrenceAnalyzerRunners;

public interface IWordOccurrenceAnalyzationRunner
{
    Task Run(Stream stream, IWordOccurrenceCounter counter, IAsyncProgressReport<long> onProgess, IProgress<Result<Okay, Exception>> onFinished, CancellationToken ct);
}
