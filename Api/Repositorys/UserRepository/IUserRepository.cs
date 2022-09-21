using Models;

namespace Repositories.UserRepository
{
    public interface IUserRepository<User>
    {
        List<User> GetAll();
        Task Insert(User entity);
        Task Update(User entity);
        Task Delete(User entity);

    }
}
