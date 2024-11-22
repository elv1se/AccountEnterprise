using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Domain.RequestFeatures;
using AccountEnterprise.Infrastructure.Extensions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Department entity) => await _dbContext.Departments.AddAsync(entity);

    public async Task<PagedList<Department>> Get(DepartmentParameters departmentParameters, bool trackChanges)
    {
        IQueryable<Department> query = _dbContext.Departments;

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByName(departmentParameters.SearchName);

        var count = await query.CountAsync();

        var departments = await query
            .Sort(departmentParameters.OrderBy)
            .Skip((departmentParameters.PageNumber - 1) * departmentParameters.PageSize)
            .Take(departmentParameters.PageSize)
            .ToListAsync();

        return new PagedList<Department>(
            departments,
            count,
            departmentParameters.PageNumber,
            departmentParameters.PageSize
        );
    }

    public async Task<Department?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Departments.Include(x => x.Employees).Include(x => x.Transactions).ThenInclude(x => x.Operation.OperationType).AsNoTracking() :
            _dbContext.Departments.Include(x => x.Employees).Include(x => x.Transactions).ThenInclude(x => x.Operation.OperationType)).SingleOrDefaultAsync(e => e.DepartmentId == id);

    public void Delete(Department entity) => _dbContext.Departments.Remove(entity);

    public void Update(Department entity) => _dbContext.Departments.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

