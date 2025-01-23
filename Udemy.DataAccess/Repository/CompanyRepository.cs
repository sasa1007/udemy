using Udemy.DataAccess.Data;
using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private AplicationDbContext _db;

    public CompanyRepository(AplicationDbContext dbContext) : base(dbContext)
    {
        _db = dbContext;
    }

    public void Update(Company company)
    {
        _db.Update(company);
    }
}