using System.Linq.Expressions;

namespace udemy.Udemy.DataAccess.Repository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(string? include = null);
    T Get(Expression<Func<T, bool>> filter, string? include = null);
    void Create(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entity);
}