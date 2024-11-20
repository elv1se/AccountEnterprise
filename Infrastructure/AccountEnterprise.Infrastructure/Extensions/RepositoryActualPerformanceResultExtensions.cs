using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using AccountEnterprise.Infrastructure.Extensions.Utility;
using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Infrastructure.Extensions;

public static class RepositoryActualPerformanceResultExtensions
{
    public static IQueryable<Operation> Search(this IQueryable<Operation> operations, string searchType, string searchMonth)
    {
        if (string.IsNullOrWhiteSpace(searchType) || string.IsNullOrWhiteSpace(searchMonth))
            return operations;


        return operations.Where(e => e.OperationType.OperationTypeId == Guid.Parse(searchType) && e.Date.Month == int.Parse(searchMonth));
    }

    public static IQueryable<Operation> Sort(this IQueryable<Operation> operations, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return operations.OrderBy(e => e.Date);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Operation>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return operations.OrderBy(e => e.Date);
        
        return operations.OrderBy(orderQuery);
    }
}
