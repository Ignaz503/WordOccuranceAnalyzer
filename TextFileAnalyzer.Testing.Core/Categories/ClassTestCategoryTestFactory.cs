namespace TextFileContentAnalyzer.Testing.Core.Categories;

public class ClassCategoryTestFactory : ITestCategoryFactory
{
    public ITestCategory CreateCategory(TestRunner suit)
        => new ClassTestCategory(suit.TestClass);
}
