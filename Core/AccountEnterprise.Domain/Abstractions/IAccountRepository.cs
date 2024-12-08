using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Domain.Abstractions;

public interface IAccountRepository 
{
    Task<PagedList<Account>> Get(AccountParameters accountParameters, bool trackChanges);
    Task<Account?> GetById(Guid id, bool trackChanges);
    Task Create(Account entity);
    void Delete(Account entity);
    void Update(Account entity);
    Task SaveChanges();
}

