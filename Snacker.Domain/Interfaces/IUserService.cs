using Snacker.Domain.Entities;

namespace Snacker.Domain.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        User ValidateUser(string email, string password);
        object Login(string email, string password);
    }
}
