using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class OperationRepository(AppDbContext dbContext) : IOperationRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Operation entity) => await _dbContext.Operations.AddAsync(entity);

    public async Task<IEnumerable<Operation>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Operations.Include(e => e.Category).Include(e => e.OperationType).AsNoTracking() 
            : _dbContext.Operations.Include(e => e.Category).Include(e => e.OperationType)).ToListAsync();

    public async Task<Operation?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Operations.Include(e => e.Category).Include(e => e.OperationType).AsNoTracking() :
            _dbContext.Operations.Include(e => e.Category).Include(e => e.OperationType)).SingleOrDefaultAsync(e => e.OperationId == id);

    public void Delete(Operation entity) => _dbContext.Operations.Remove(entity);

    public void Update(Operation entity) => _dbContext.Operations.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

