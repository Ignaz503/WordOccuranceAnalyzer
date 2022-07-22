using TextFileContentAnalyzer.Testing.Core.Categories;
using TextFileContentAnalyzer.Testing.Core.Reporting;
using TextFileContentAnalyzer.Testing.Core;

var ctx = new TestContext()
{
    CategoryFactory = new ClassCategoryTestFactory(),
    Reporter = new CategorizedConsoleTestReporter()
};

TestRegistry.BuildRegistry(typeof(Program).Assembly);
await TestRegistry.Execute(ctx);

ctx.Reporter.DisplayResults();
