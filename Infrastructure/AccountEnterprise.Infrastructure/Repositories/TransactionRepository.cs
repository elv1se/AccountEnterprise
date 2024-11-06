using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class TransactionRepository(AppDbContext dbContext) : ITransactionRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Transaction entity) => await _dbContext.Transactions.AddAsync(entity);

    public async Task<IEnumerable<Transaction>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Transactions.Include(e => e.Operation).Include(e => e.Department).AsNoTracking() 
            : _dbContext.Transactions.Include(e => e.Operation).Include(e => e.Department)).ToListAsync();

    public async Task<Transaction?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Transactions.Include(e => e.Operation).Include(e => e.Department).AsNoTracking() :
            _dbContext.Transactions.Include(e => e.Operation).Include(e => e.Department)).SingleOrDefaultAsync(e => e.TransactionId == id);

    public void Delete(Transaction entity) => _dbContext.Transactions.Remove(entity);

    public void Update(Transaction entity) => _dbContext.Transactions.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

