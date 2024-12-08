using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Domain.Abstractions;

public interface IEmployeeRepository 
{
    Task<PagedList<Employee>> Get(EmployeeParameters employeeParameters, bool trackChanges);
    Task<Employee?> GetById(Guid id, bool trackChanges);
    Task Create(Employee entity);
    void Delete(Employee entity);
    void Update(Employee entity);
    Task SaveChanges();
}

