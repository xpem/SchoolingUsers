using Models;

namespace Domain.Services
{
    public interface IUserService
    {

        List<User> GetUsers();

        User? GetUser(int id);

        Result AddUser(User user);

        Result UpdateUser(User user);

        void Delete(int id);

    }
}
