using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Domain.Abstractions;

public interface IAccountRepository 
{
	Task<IEnumerable<Account>> Get(bool trackChanges);
	Task<Account?> GetById(Guid id, bool trackChanges);
    Task Create(Account entity);
    void Delete(Account entity);
    void Update(Account entity);
    Task SaveChanges();
}

