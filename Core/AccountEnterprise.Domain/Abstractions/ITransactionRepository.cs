using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Domain.Abstractions;

public interface ITransactionRepository 
{
	Task<IEnumerable<Transaction>> Get(bool trackChanges);
	Task<Transaction?> GetById(Guid id, bool trackChanges);
    Task Create(Transaction entity);
    void Delete(Transaction entity);
    void Update(Transaction entity);
    Task SaveChanges();
}

