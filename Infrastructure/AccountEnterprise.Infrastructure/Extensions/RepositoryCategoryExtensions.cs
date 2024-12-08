using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Infrastructure.Extensions.Utility;
namespace AccountEnterprise.Infrastructure.Extensions;

public static class RepositoryCategoryExtensions
{
    public static IQueryable<Category> SearchByName(this IQueryable<Category> categories, string searchName)
    {
        if (string.IsNullOrWhiteSpace(searchName))
            return categories;

        var lowerCaseTerm = searchName.Trim().ToLower();

        return categories.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Category> Sort(this IQueryable<Category> categories, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return categories.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Category>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return categories.OrderBy(e => e.Name);

        return categories.OrderBy(orderQuery);
    }
}
