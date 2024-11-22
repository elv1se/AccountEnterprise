using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Domain.Abstractions;

public interface IDepartmentRepository 
{
    Task<PagedList<Department>> Get(DepartmentParameters departmentParameters, bool trackChanges);
    Task<Department?> GetById(Guid id, bool trackChanges);
    Task Create(Department entity);
    void Delete(Department entity);
    void Update(Department entity);
    Task SaveChanges();
}

