using System.Linq.Expressions;
using Cards.DataAccessLayer.Contexts;
using Cards.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cards.DataAccessLayer.Repositories.Implementations;

public class RepositoryBase<T>(RepositoryContext context) : IRepositoryBase<T> where T : class
{
    public IQueryable<T> FindAll(int pageNumber, int pageSize, bool trackChanges)
    {
        return trackChanges
            ? context
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
            : context
                .Set<T>()
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
    }

    public IQueryable<T> FindSingleByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return trackChanges
            ? context
                .Set<T>()
                .Where(expression)
            : context
                .Set<T>()
                .Where(expression)
                .AsNoTracking();
    } 

    public void Create(T entity) => 
        context.Set<T>().Add(entity);

    public void Update(T entity) => 
        context.Set<T>().Update(entity);

    public void Delete(T entity) => 
        context.Set<T>().Remove(entity);
}