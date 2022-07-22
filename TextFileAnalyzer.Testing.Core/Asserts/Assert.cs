using System.Runtime.CompilerServices;

namespace TextFileContentAnalyzer.Testing.Core.Asserts;

public static class Assert
{
    public static void IsTrue(bool value, [CallerArgumentExpression("value")]string expression = "" ) 
    {
        if(!value)
            throw new AssertException($"{expression} should be true"); 
    }

    public static void IsTrue(bool value, string message, [CallerArgumentExpression("value")] string expression = "")
    {
        if (!value)
            throw new AssertException($"{expression} should be true. {message}");
    }

    public static void IsFalse(bool value, [CallerArgumentExpression("value")] string expression = "")
    {
        if (value)
            throw new AssertException($"{expression} should be false");
    }

    public static void IsFalse(bool value, string message, [CallerArgumentExpression("value")] string expression = "")
    {
        if (value)
            throw new AssertException($"{expression} should be false. {message}");
    }

    public static void Throw(Exception ex, string message = "")
    {
        if (string.IsNullOrEmpty(message))
            throw new AssertException(ex.ToString());
        throw new AssertException(message, ex);
    }
}
