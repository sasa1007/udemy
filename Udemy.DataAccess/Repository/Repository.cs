using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Udemy.DataAccess.Data;

namespace udemy.Udemy.DataAccess.Repository;




public class Repository<T> : IRepository<T> where T : class
{
    private readonly AplicationDbContext _db;

    internal DbSet<T> dbSet;

    public Repository(AplicationDbContext dbContext)
    {
        _db = dbContext;
        this.dbSet = _db.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public void Create(T entity)
    {
        dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}