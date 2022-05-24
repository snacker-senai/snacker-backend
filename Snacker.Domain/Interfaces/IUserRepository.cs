using Snacker.Domain.Entities;

namespace Snacker.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User ValidateUser(string email, string password);
    }
}
