using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileContentAnalyzer.Core.Util;

/// <summary>
/// Asynchrounous progress reporting
/// </summary>
/// <typeparam name="T">Type of progress to report</typeparam>
public interface IAsyncProgressReport<T>
{
    Task Report(T value);
}
