namespace TextFileContentAnalyzer.Testing.Core.Categories;

public class ClassTestCategory : ITestCategory
{
    readonly Type @class;
    public ClassTestCategory(Type @class)
    {
        this.@class = @class;
    }
    public bool Equals(ITestCategory? other)
    {
        if (other is ClassTestCategory cCat)
            return @class.Equals(cCat.@class);
        return false;
    }

    public override int GetHashCode()
        => @class.GetHashCode();

    public override string ToString()
        => @class.Name;

}
