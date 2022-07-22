using System.Diagnostics;

namespace TextFileContentAnalyzer.Core.Util;

/// <summary>
/// Imprecise timer that uses Stopwatch.GetTimestamp() to do it's timing
/// </summary>
public class TickBasedTimer : ITimer
{

    long ticksToFire;
    public long MillisecondsToFire { get =>ticksToFire / 10_000; set => ticksToFire = value * 10_000; }

    long timeStampFromLastFire;

    public TickBasedTimer()
    {
        timeStampFromLastFire = Stopwatch.GetTimestamp();
    }

    public bool Meassure()
    {
        var currentTimeStamp = Stopwatch.GetTimestamp();    

        var diff = currentTimeStamp - timeStampFromLastFire;

        var res = diff >= ticksToFire;

        if (res)
            timeStampFromLastFire = currentTimeStamp;
        return res;
    }
}