using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class OperationTypeRepository(AppDbContext dbContext) : IOperationTypeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(OperationType entity) => await _dbContext.OperationTypes.AddAsync(entity);

    public async Task<IEnumerable<OperationType>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.OperationTypes.AsNoTracking() 
            : _dbContext.OperationTypes).ToListAsync();

    public async Task<OperationType?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.OperationTypes.AsNoTracking() :
            _dbContext.OperationTypes).SingleOrDefaultAsync(e => e.OperationTypeId == id);

    public void Delete(OperationType entity) => _dbContext.OperationTypes.Remove(entity);

    public void Update(OperationType entity) => _dbContext.OperationTypes.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

