using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileContentAnalyzer.Core.Optional;

/// <summary>
/// Represents the result of an operation, that might not return a value
/// </summary>
/// <typeparam name="T">Result Type</typeparam>
public struct Result<T>
{
    bool hasValue;
    public bool HasValue => hasValue;

    public T? Value { get; private set; }

    public Result()
    {
        Value = default;
        hasValue = false;
    }

    public Result(T t) 
    {
        hasValue = true;
        Value = t;
    }

    public void Match(Action<T> onValue, Action onNone) 
    {
        if (hasValue)
            onValue(Value!);
        else
            onNone();
    }

    public T? Unwrap() 
    {
        if (hasValue)
            return Value;
        return default;
    }
}

/// <summary>
/// Represents the result of an operation, that might not return a value
/// and instead returns an exception indicator.
/// </summary>
/// <typeparam name="T">Result Type</typeparam>
/// <typeparam name="TException">The exception type if failed to produce a result</typeparam>
public struct Result<T, TException> 
{
    bool hasValue;
    public bool HasValue => hasValue;

    public T? Value { get; private set; }
    public TException? Exception { get; private set; }
    public Result()
    {
        Value = default;
        Exception = default;
        hasValue = false;
    }

    public Result(T t)
    {
        hasValue = true;
        Value = t;
        Exception = default;
    }

    public Result(TException? exc)
    {
        hasValue = false;
        Value = default;
        Exception = exc;
    }

    /// <summary>
    /// matches the result based on having a value or an exception
    /// </summary>
    /// <param name="onValue"></param>
    /// <param name="onNone"></param>
    public void Match(Action<T> onValue, Action<TException?> onNone)
    {
        if (hasValue)
            onValue(Value!);
        else
            onNone(Exception);
    }

    /// <summary>
    /// unwraps result into value even if none present.
    /// Use Match instead
    /// </summary>
    /// <returns></returns>
    public T? Unwrap()
    {
        if (hasValue)
            return Value;
        return default;
    }
}

