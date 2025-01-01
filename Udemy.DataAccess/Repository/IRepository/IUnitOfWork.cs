namespace udemy.Udemy.DataAccess.Repository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }

    void Save();
}