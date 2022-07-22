namespace TextFileContentAnalyzer.Core.Optional;

/// <summary>
/// Extensions to create results
/// </summary>
public static class ResultExtensions 
{
    /// <summary>
    /// creates a result based on a value being null or not
    /// </summary>
    /// <typeparam name="T">The result object type</typeparam>
    /// <param name="obj">the object the result is built from</param>
    /// <returns>A result with value if obj is not null, or a result without value if null</returns>
    public static Result<T> From<T>(this T? obj)
    {
        if (obj is null)
            return obj.None();
        return obj.Some();
    }

    /// <summary>
    /// creates result with value from object
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <param name="obj">The object instance</param>
    /// <returns>a result with value</returns>
    public static Result<T> Some<T>(this T obj)
        => new(obj);

    /// <summary>
    /// creates result without value from object
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <param name="obj">The object instance</param>
    /// <returns>a result without a value</returns>
    public static Result<T> None<T>(this T? obj)
        => new();

    /// <summary>
    /// creates result with value from object
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <typeparam name="TException">The type of exception the result might be</typeparam>
    /// <param name="obj">The object instance</param>
    /// <returns>a result with value</returns>
    public static Result<T, TException> Some<T, TException>(this T obj)
    => new(obj);

    /// <summary>
    /// creates result without value from object
    /// consider using Result<typeparamref name="T"/> if no exception is specified 
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <typeparam name="TException">The type of exception the result might be</typeparam>
    /// <param name="obj">The object instance</param>
    /// <returns>a result without value and no exception specified</returns>
    public static Result<T, TException> None<T, TException>(this T? obj)
        => new();

    /// <summary>
    /// creates result without value from object
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <typeparam name="TException">The type of exception the result might be</typeparam>
    /// <param name="obj">The object instance</param>
    /// <returns>a result without value and an exception specified</returns>
    public static Result<T, TException> None<T, TException>(this T? obj, TException? exception)
        => new(exception);

    /// <summary>
    /// creates result based on value being null or not
    /// consider using Result<typeparamref name="T"/> if no exception is specified 
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <typeparam name="TException">The type of exception the result might be</typeparam>
    /// <param name="obj">The object instance</param>
    /// <returns>a result</returns>
    public static Result<T, TException> From<T, TException>(this T? obj)
    {
        if (obj is null)
            return obj.None<T, TException>();
        return obj.Some<T, TException>();
    }

    /// <summary>
    /// creates result based on value being null or not
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <typeparam name="TException">The type of exception the result might be</typeparam>
    /// <param name="obj">The object instance</param>
    /// <param name="exception">The exception the result should report</param>
    /// <returns>A result</returns>
    public static Result<T, TException> From<T, TException>(this T? obj, TException? exception)
    {
        if (obj is null)
            return obj.None<T, TException>(exception);
        return obj.Some<T, TException>();
    }

    /// <summary>
    /// creates result based on the exception type.
    /// Result will have no value.
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <typeparam name="TException">The type of exception the result might be</typeparam>
    /// <param name="exception">The exception the result should report</param>
    /// <returns>A result with no value</returns>
    public static Result<T, TException> From<T, TException>(this TException?  exception)
        => default(T).None(exception);
    
}

