namespace TextFileContentAnalyzer.Core.Mediator;

/// <summary>
/// delegate for a synchronous message handler
/// </summary>
/// <typeparam name="T">Type of message</typeparam>
/// <param name="message">Instance of Message</param>
public delegate void MessageHandler<T>(T message);

/// <summary>
/// delegate for asynchrounous message handler
/// </summary>
/// <typeparam name="T">Type of message</typeparam>
/// <param name="message">Instance of Message</param>
/// <returns>An awaitable Task</returns>
public delegate Task AsyncMessageHandler<T>(T message);


