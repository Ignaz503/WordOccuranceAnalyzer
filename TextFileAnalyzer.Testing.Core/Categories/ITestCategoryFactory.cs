namespace TextFileContentAnalyzer.Testing.Core.Categories;

public interface ITestCategoryFactory
{
    ITestCategory CreateCategory(TestRunner suit);
}
