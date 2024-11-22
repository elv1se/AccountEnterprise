using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using AccountEnterprise.Infrastructure.Extensions.Utility;
using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Infrastructure.Extensions;

public static class RepositoryAccountExtensions
{
    public static IQueryable<Account> SearchByNumber(this IQueryable<Account> accounts, string searchNumber)
    {
        if (string.IsNullOrWhiteSpace(searchNumber))
            return accounts;

        var lowerCaseTerm = searchNumber.Trim().ToLower();

        return accounts.Where(e => e.Number.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Account> Sort(this IQueryable<Account> accounts, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return accounts.OrderBy(e => e.Number);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Account>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return accounts.OrderBy(e => e.Number);
        
        return accounts.OrderBy(orderQuery);
    }
}
