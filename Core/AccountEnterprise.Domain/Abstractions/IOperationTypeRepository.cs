using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Domain.Abstractions;

public interface IOperationTypeRepository 
{
	Task<IEnumerable<OperationType>> Get(bool trackChanges);
	Task<OperationType?> GetById(Guid id, bool trackChanges);
    Task Create(OperationType entity);
    void Delete(OperationType entity);
    void Update(OperationType entity);
    Task SaveChanges();
}

