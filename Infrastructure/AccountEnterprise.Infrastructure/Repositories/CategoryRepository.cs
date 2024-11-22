using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Domain.RequestFeatures;
using AccountEnterprise.Infrastructure.Extensions;

namespace AccountEnterprise.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Category entity) => await _dbContext.Categories.AddAsync(entity);

    public async Task<IEnumerable<Category>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Categories.AsNoTracking() 
            : _dbContext.Categories).ToListAsync();

    public async Task<PagedList<Category>> Get(CategoryParameters categoryParameters, bool trackChanges)
    {
        IQueryable<Category> query = _dbContext.Categories;

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByName(categoryParameters.SearchName);

        var count = await query.CountAsync();

        var categorys = await query
            .Sort(categoryParameters.OrderBy)
            .Skip((categoryParameters.PageNumber - 1) * categoryParameters.PageSize)
            .Take(categoryParameters.PageSize)
            .ToListAsync();

        return new PagedList<Category>(
            categorys,
            count,
            categoryParameters.PageNumber,
            categoryParameters.PageSize
        );
    }

    public async Task<Category?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Categories.AsNoTracking() :
            _dbContext.Categories).SingleOrDefaultAsync(e => e.CategoryId == id);

    public void Delete(Category entity) => _dbContext.Categories.Remove(entity);

    public void Update(Category entity) => _dbContext.Categories.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

