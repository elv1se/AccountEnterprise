using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Domain.Abstractions;

public interface IOperationRepository 
{
    Task<IEnumerable<Operation>> Get(OperationParameters operationParameters, bool trackChanges);

    Task<Operation?> GetById(Guid id, bool trackChanges);
    Task Create(Operation entity);
    void Delete(Operation entity);
    void Update(Operation entity);
    Task SaveChanges();
}

