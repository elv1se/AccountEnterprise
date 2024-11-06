using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class AccountRepository(AppDbContext dbContext) : IAccountRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Account entity) => await _dbContext.Accounts.AddAsync(entity);

    public async Task<IEnumerable<Account>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Accounts.Include(e => e.Department).AsNoTracking() 
            : _dbContext.Accounts.Include(e => e.Department)).ToListAsync();

    public async Task<Account?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Accounts.Include(e => e.Department).AsNoTracking() :
            _dbContext.Accounts.Include(e => e.Department)).SingleOrDefaultAsync(e => e.AccountId == id);

    public void Delete(Account entity) => _dbContext.Accounts.Remove(entity);

    public void Update(Account entity) => _dbContext.Accounts.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

