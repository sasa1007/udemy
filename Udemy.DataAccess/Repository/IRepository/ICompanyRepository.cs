using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;

public interface ICompanyRepository: IRepository<Company>
{
    void Update(Company company);
}