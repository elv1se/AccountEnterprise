using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Domain.RequestFeatures;
using AccountEnterprise.Infrastructure.Extensions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class TransactionRepository(AppDbContext dbContext) : ITransactionRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Transaction entity) => await _dbContext.Transactions.AddAsync(entity);

    public async Task<PagedList<Transaction>> Get(TransactionParameters transactionParameters, bool trackChanges)
    {
        IQueryable<Transaction> query = _dbContext.Transactions.Include(x => x.Department).Include(x => x.Operation);

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByType(transactionParameters.SearchType);

        var count = await query.CountAsync();

        var transactions = await query
            .Sort(transactionParameters.OrderBy)
            .Skip((transactionParameters.PageNumber - 1) * transactionParameters.PageSize)
            .Take(transactionParameters.PageSize)
            .ToListAsync();

        return new PagedList<Transaction>(
            transactions,
            count,
            transactionParameters.PageNumber,
            transactionParameters.PageSize
        );
    }

    public async Task<Transaction?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Transactions.Include(e => e.Operation).Include(e => e.Department).AsNoTracking() :
            _dbContext.Transactions.Include(e => e.Operation).Include(e => e.Department)).SingleOrDefaultAsync(e => e.TransactionId == id);

    public void Delete(Transaction entity) => _dbContext.Transactions.Remove(entity);

    public void Update(Transaction entity) => _dbContext.Transactions.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

