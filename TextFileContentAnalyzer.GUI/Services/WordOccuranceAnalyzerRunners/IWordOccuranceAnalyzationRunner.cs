using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;
using TextFileContentAnalyzer.Core.Optional;
using TextFileContentAnalyzer.Core.Util;

namespace TextFileContentAnalyzer.GUI.Services.WordOccuranceAnalyzerRunners;

public interface IWordOccuranceAnalyzationRunner
{
    Task Run(Stream stream, IWordOccuranceCounter counter, IAsyncProgressReport<long> onProgess, IProgress<Result<Okay, Exception>> onFinished, CancellationToken ct);
}
