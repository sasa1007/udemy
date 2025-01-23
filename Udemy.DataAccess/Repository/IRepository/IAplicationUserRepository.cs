using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;

public interface IAplicationUserRepository : IRepository<ApplicationUser>
{
    void Update(ApplicationUser applicationUser);

}