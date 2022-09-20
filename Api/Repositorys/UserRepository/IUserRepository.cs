using Models;

namespace Repositories.UserRepository
{
    public interface IUserRepository<User>
    {
        List<User> GetAll();
        void Insert(User entity);
        void Update(User entity);
        void Delete(User entity);

    }
}
