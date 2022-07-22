using System.Threading;

namespace TextFileContentAnalyzer.GUI.Commands;

/// <summary>
/// Execution context of an asynchrounous command
/// </summary>
public struct AsyncCommandExecutionContext 
{
    public object? Parameter { get; init; }
    public CancellationToken Token { get; init; }
}
