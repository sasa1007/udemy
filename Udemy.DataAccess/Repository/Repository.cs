using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Udemy.DataAccess.Data;

namespace udemy.Udemy.DataAccess.Repository;




public class Repository<T> : IRepository<T> where T : class
{
    private readonly AplicationDbContext _db;

    internal DbSet<T> DbSet;

    public Repository(AplicationDbContext dbContext)
    {
        _db = dbContext;
        this.DbSet = _db.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = DbSet;
        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = DbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public void Create(T entity)
    {
        DbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entity)
    {
        DbSet.RemoveRange(entity);
    }
}