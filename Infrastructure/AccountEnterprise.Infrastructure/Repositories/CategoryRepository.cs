using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Category entity) => await _dbContext.Categories.AddAsync(entity);

    public async Task<IEnumerable<Category>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Categories.AsNoTracking() 
            : _dbContext.Categories).ToListAsync();

    public async Task<Category?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Categories.AsNoTracking() :
            _dbContext.Categories).SingleOrDefaultAsync(e => e.CategoryId == id);

    public void Delete(Category entity) => _dbContext.Categories.Remove(entity);

    public void Update(Category entity) => _dbContext.Categories.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

