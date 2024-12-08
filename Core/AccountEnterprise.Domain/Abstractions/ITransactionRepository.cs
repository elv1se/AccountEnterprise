using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Domain.Abstractions;

public interface ITransactionRepository 
{
    Task<PagedList<Transaction>> Get(TransactionParameters transactionParameters, bool trackChanges);
    Task<Transaction?> GetById(Guid id, bool trackChanges);
    Task Create(Transaction entity);
    void Delete(Transaction entity);
    void Update(Transaction entity);
    Task SaveChanges();
}

