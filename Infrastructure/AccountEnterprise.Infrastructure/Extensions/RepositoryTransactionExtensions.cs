using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Infrastructure.Extensions.Utility;

namespace AccountEnterprise.Infrastructure.Extensions;

public static class RepositoryTransactionExtensions
{
    public static IQueryable<Transaction> SearchByType(this IQueryable<Transaction> transactions, string searchType)
    {
        if (string.IsNullOrWhiteSpace(searchType))
            return transactions;

        var lowerCaseTerm = searchType.Trim().ToLower();

        return transactions.Where(e => e.Type.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Transaction> Sort(this IQueryable<Transaction> transactions, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return transactions.OrderBy(e => e.Type);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Transaction>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return transactions.OrderBy(e => e.Type);

        return transactions.OrderBy(orderQuery);
    }
}
