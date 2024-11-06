using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Department entity) => await _dbContext.Departments.AddAsync(entity);

    public async Task<IEnumerable<Department>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Departments.AsNoTracking() 
            : _dbContext.Departments).ToListAsync();

    public async Task<Department?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Departments.AsNoTracking() :
            _dbContext.Departments).SingleOrDefaultAsync(e => e.DepartmentId == id);

    public void Delete(Department entity) => _dbContext.Departments.Remove(entity);

    public void Update(Department entity) => _dbContext.Departments.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

