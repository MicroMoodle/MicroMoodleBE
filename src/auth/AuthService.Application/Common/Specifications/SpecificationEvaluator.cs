using AuthService.Application.Common.Interfaces;
using AuthService.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Application.Common.Specifications;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;

        // modify the IQueryable using the specification's criteria expression
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Includes all expression-based includes
        query = specification.Includes.Aggregate(query,
            (current, include) => current.Include(include));

        // Include any string-based include statements
        query = specification.IncludeStrings.Aggregate(query,
            (current, include) => current.Include(include));

        // Apply ordering if expressions are set
        if (specification.OrderBy.Count != 0)
        {
            query = specification.OrderBy.Aggregate(query,
                (current, orderBy) => current.OrderBy(orderBy));
        }
        else if (specification.OrderByDescending.Count != 0)
        {
            query = specification.OrderByDescending.Aggregate(query,
                (current, orderByDescending) => current.OrderByDescending(orderByDescending));
        }
        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        // Apply paging if enabled
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip)
                .Take(specification.Take);
        }
        return query;
    }
}
