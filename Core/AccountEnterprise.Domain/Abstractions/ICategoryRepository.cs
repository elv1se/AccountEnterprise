using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Domain.Abstractions;

public interface ICategoryRepository 
{
	Task<IEnumerable<Category>> Get(bool trackChanges);
	Task<Category?> GetById(Guid id, bool trackChanges);
    Task Create(Category entity);
    void Delete(Category entity);
    void Update(Category entity);
    Task SaveChanges();
}

