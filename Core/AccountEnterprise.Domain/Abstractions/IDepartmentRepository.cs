using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Domain.Abstractions;

public interface IDepartmentRepository 
{
	Task<IEnumerable<Department>> Get(bool trackChanges);
	Task<Department?> GetById(Guid id, bool trackChanges);
    Task Create(Department entity);
    void Delete(Department entity);
    void Update(Department entity);
    Task SaveChanges();
}

