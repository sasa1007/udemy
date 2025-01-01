using System.Linq.Expressions;

namespace udemy.Udemy.DataAccess.Repository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Get(Expression<Func<T, bool>> filter);
    void Create(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entity);
}