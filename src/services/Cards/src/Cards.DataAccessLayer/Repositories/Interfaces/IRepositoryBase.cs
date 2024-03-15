using System.Linq.Expressions;

namespace Cards.DataAccessLayer.Repositories.Interfaces;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(int pageNumber, int pageSize, bool trackChanges);
    IQueryable<T> FindSingleByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}