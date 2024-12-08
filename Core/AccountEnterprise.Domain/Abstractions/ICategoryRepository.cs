using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Domain.Abstractions;

public interface ICategoryRepository 
{
    Task<PagedList<Category>> Get(CategoryParameters categoryParameters, bool trackChanges);
    Task<Category?> GetById(Guid id, bool trackChanges);
    Task Create(Category entity);
    void Delete(Category entity);
    void Update(Category entity);
    Task SaveChanges();
}

