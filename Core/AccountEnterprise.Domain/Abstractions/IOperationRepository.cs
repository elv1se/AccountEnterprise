using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Domain.Abstractions;

public interface IOperationRepository 
{
	Task<IEnumerable<Operation>> Get(bool trackChanges);
	Task<Operation?> GetById(Guid id, bool trackChanges);
    Task Create(Operation entity);
    void Delete(Operation entity);
    void Update(Operation entity);
    Task SaveChanges();
}

