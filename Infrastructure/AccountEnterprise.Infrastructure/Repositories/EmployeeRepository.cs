using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class EmployeeRepository(AppDbContext dbContext) : IEmployeeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Employee entity) => await _dbContext.Employees.AddAsync(entity);

    public async Task<IEnumerable<Employee>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Employees.Include(e => e.Department).AsNoTracking() 
            : _dbContext.Employees.Include(e => e.Department)).ToListAsync();

    public async Task<Employee?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Employees.Include(e => e.Department).AsNoTracking() :
            _dbContext.Employees.Include(e => e.Department)).SingleOrDefaultAsync(e => e.EmployeeId == id);

    public void Delete(Employee entity) => _dbContext.Employees.Remove(entity);

    public void Update(Employee entity) => _dbContext.Employees.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

