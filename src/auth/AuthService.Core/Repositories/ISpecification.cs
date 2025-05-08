using System.Linq.Expressions;

namespace AuthService.Core.Repositories;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }

    List<Expression<Func<T, object>>> Includes { get; }

    List<string> IncludeStrings { get; }

    List<Expression<Func<T, object>>> OrderBy { get; }

    List<Expression<Func<T, object>>> OrderByDescending { get; }

    Expression<Func<T, object>> GroupBy { get; }

    int Take { get; }

    int Skip { get; }

    bool IsPagingEnabled { get; }
}
