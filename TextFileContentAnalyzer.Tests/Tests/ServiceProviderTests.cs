
using TextFileContentAnalyzer.Core.ServiceProvider;
using TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;
using TextFileContentAnalyzer.Testing.Core;
using TextFileContentAnalyzer.Testing.Core.Asserts;

namespace TextFileContentAnalyzer.Testing.Tests;

[Test]
public class ServiceProviderTests
{
    #region local objects
    public class TestObject
    { }

    public class CircularDependencyA
    {
        public CircularDependencyA(CircularDependencyB b)
        { }
    }

    public class CircularDependencyB
    {
        public CircularDependencyB(CircularDependencyA a)
        { }
    }
    #endregion

    [Fact]
    public void SingletonService_ShouldBe_SameReferenceEveryTime_WhenAddedViaObjectInstance()
    {

        var obj = new TestObject();
        var sp = new ServiceProviderBuilder().AddSingleton(obj).Build();

        var servie = sp.GetService<TestObject>();
        Assert.IsTrue(obj == servie);
    }

    [Fact]
    public void SingletonService_ShouldBe_SameReferenceEveryTime_WhenAddedViaObject()
    {

        var sp = new ServiceProviderBuilder().AddSingleton<TestObject>().Build();

        var first = sp.GetService<TestObject>();
        var second = sp.GetService<TestObject>();
        Assert.IsTrue(first == second);
    }
    [Fact]
    public void SingletonService_ShouldBe_SameReferenceEveryTime_WhenAddedViaFactory()
    {

        var sp = new ServiceProviderBuilder().AddSingleton<TestObject>((p) => new()).Build();

        var first = sp.GetService<TestObject>();
        var second = sp.GetService<TestObject>();
        Assert.IsTrue(first == second);
    }

    [Fact]
    public void ServiceBuilderShould_ThrowMultiRegister_WhenAddingServiceTwice()
    {
        try
        {
            var sp = new ServiceProviderBuilder().AddSingleton<TestObject>((p) => new()).AddTransient<TestObject>().Build();
        }
        catch (Exception ex)
        {

            Assert.IsTrue(ex.GetType() == typeof(ServiceMultiRegisterException), message: $"actualy type {ex.GetType()}");
            return;
        }
        Assert.IsTrue(false, "This should never be reached expecting exception");
    }

    [Fact]
    public void ServiceBuilderShould_ThrowInvalidNodeRegisteredExteption_WhenAddingServiceWithCircularDependency()
    {
        try
        {
            var sp = new ServiceProviderBuilder().AddSingleton<CircularDependencyA>().AddSingleton<CircularDependencyB>().Build();
        }
        catch (Exception ex)
        {

            Assert.IsTrue(ex.GetType() == typeof(InvalidNodeRegisteredExteption), message: $"actualy type {ex.GetType()}");
            return;
        }
        Assert.IsTrue(false, "This should never be reached expecting exception");
    }

    [Fact]
    public void TransientService_ShouldBe_DifferentReferenceEveryTime_WhenAddedViaFactory()
    {

        var sp = new ServiceProviderBuilder().AddTransient<TestObject>((p) => new()).Build();

        var first = sp.GetService<TestObject>();
        var second = sp.GetService<TestObject>();
        Assert.IsTrue(first != second);
    }

    [Fact]
    public void TransientService_ShouldBe_DifferentReferenceEveryTime_WhenAddedViaObject()
    {

        var sp = new ServiceProviderBuilder().AddTransient<TestObject>().Build();

        var first = sp.GetService<TestObject>();
        var second = sp.GetService<TestObject>();
        Assert.IsTrue(first != second);
    }

    [Fact]
    public void EmptyServiceProviderBuilderShould_NotCrash()
    {
        var sp = new ServiceProviderBuilder().Build();
        Assert.IsTrue(true, message: "Never see this message");
    }
}
