using Udemy.DataAccess.Data;
using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;




public class AplicationUserRepository : Repository<ApplicationUser>, IAplicationUserRepository
{
    private AplicationDbContext _db;
    public AplicationUserRepository(AplicationDbContext dbContext) : base(dbContext)
    {
        _db = dbContext;
    }

    public void Update(ApplicationUser applicationUser)
    {
        _db.ApplicationUsers.Update(applicationUser);
    }

}