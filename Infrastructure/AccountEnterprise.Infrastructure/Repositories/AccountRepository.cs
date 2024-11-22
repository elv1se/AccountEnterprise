using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Domain.RequestFeatures;
using AccountEnterprise.Infrastructure.Extensions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class AccountRepository(AppDbContext dbContext) : IAccountRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Account entity) => await _dbContext.Accounts.AddAsync(entity);

    public async Task<PagedList<Account>> Get(AccountParameters accountParameters, bool trackChanges)
    {
        IQueryable<Account> query = _dbContext.Accounts.Include(e => e.Department);

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByNumber(accountParameters.SearchNumber);

        var count = await query.CountAsync();

        var accounts = await query
            .Sort(accountParameters.OrderBy)
            .Skip((accountParameters.PageNumber - 1) * accountParameters.PageSize)
            .Take(accountParameters.PageSize)
            .ToListAsync();

        return new PagedList<Account>(
            accounts,
            count,
            accountParameters.PageNumber,
            accountParameters.PageSize
        );
    }



    public async Task<Account?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Accounts.Include(e => e.Department).AsNoTracking() :
            _dbContext.Accounts.Include(e => e.Department)).SingleOrDefaultAsync(e => e.AccountId == id);

    public void Delete(Account entity) => _dbContext.Accounts.Remove(entity);

    public void Update(Account entity) => _dbContext.Accounts.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

