namespace TextFileContentAnalyzer.Core.Util;

/// <summary>
/// Simple timer
/// </summary>
public interface ITimer 
{
    /// <summary>
    /// Milliseconds between fires
    /// </summary>
    long MillisecondsToFire { get; set; }
    /// <summary>
    /// meassures time between last fire and now
    /// </summary>
    /// <returns>true if the time fired</returns>
    bool Meassure();
}
