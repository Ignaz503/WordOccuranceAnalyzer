using TextFileContentAnalyzer.Testing.Core.Asserts;
using TextFileContentAnalyzer.Testing.Core;
using TextFileContentAnalyzer.Core.Optional;

namespace TextFileContentAnalyzer.Tests.Tests;

[Test]
public class OptionalTests 
{
    [Fact]
    public void Result_HasValue_IfSomeResult() 
    {
        var integer  = 12;
        var res = integer.Some();

        Assert.IsTrue(res.HasValue);
    }
    [Fact]
    public void Result_HasNoValue_IfNoneResult()
    {
        var integer = 12;
        var res = integer.None();

        Assert.IsFalse(res.HasValue);
    }

    [Fact]
    public void Result_HasValue_IfFromWithNonNull()
    {
        var integer = 12;
        var res = integer.From();

        Assert.IsTrue(res.HasValue);
    }

    [Fact]
    public void Result_HasNoValue_IfFromWithNull()
    {
        int? integer = null;
        var res = integer.From();

        Assert.IsFalse(res.HasValue);
    }

    [Fact]
    public void Result_HasNoValue_IfNoneCalledOnNull()
    {
        object? obj = null;
        var res = obj.None();

        Assert.IsFalse(res.HasValue);
    }


    [Fact]
    public void Result_MatchesOnValue_IfResultHasValue()
    {
        var obj = new object();
        var res = obj.Some();

        bool shouldBeTrue = false;

        res.Match(i => shouldBeTrue = true, () => shouldBeTrue = false);

        Assert.IsTrue(shouldBeTrue);
    }

    [Fact]
    public void Result_MatchesOnNoValue_IfResultHasNoValue()
    {
        var obj = new object();
        var res = obj.None();

        bool shouldBeTrue = true;

        res.Match(i => shouldBeTrue = true, () => shouldBeTrue = false);

        Assert.IsFalse(shouldBeTrue);
    }

    [Fact]
    public void ResultWithException_HasValue_IfSomeResult()
    {
        var integer = 12;
        var res = integer.Some<int, Exception>();

        Assert.IsTrue(res.HasValue);
    }
    [Fact]
    public void ResultWithException_HasNoValue_IfNoneResult()
    {
        var integer = 12;
        var res = integer.None<int, Exception>();

        Assert.IsFalse(res.HasValue);
    }
     
    [Fact]
    public void ResultWithException_HasValue_IfFromCalled()
    {
        var obj = new object();
        var res = obj.From<object,Exception>();

        Assert.IsTrue(res.HasValue);
    }

    [Fact]
    public void ResultWithException_HasNoValue_IfFromCalledOnNull()
    {
        object? integer = null;
        var res = integer.From<object, Exception>();

        Assert.IsFalse(res.HasValue);
    }

    [Fact]
    public void ResultWithException_MatchesOnValue_IfResultHasValue()
    {
        var obj = new object();
        var res = obj.Some<object>();

        bool shouldBeTrue = false;

        res.Match(i => shouldBeTrue = true, () => shouldBeTrue = false);

        Assert.IsTrue(shouldBeTrue);
    }

    [Fact]
    public void ResultWithException_MatchesOnNoValue_IfResultHasNoValue()
    {
        object? obj = null;
        var res = obj.None<object,Exception>();

        bool shouldBeTrue = true;

        res.Match(i => shouldBeTrue = true, (exc) => shouldBeTrue = false);

        Assert.IsFalse(shouldBeTrue);
    }

}
