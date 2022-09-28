using Models;
using Repositories.Contexts;
using Repositories.UserRepository;

namespace Domain.Services
{
    public class UserService : IUserService
    {

        #region Property
        private readonly IUserRepository<User> UserRepository;
        private readonly UserContext UserContext;
        #endregion

        #region Constructor
        public UserService(IUserRepository<User> userRepository, UserContext userContext)
        {
            UserRepository = userRepository;
            UserContext = userContext;
        }

        #endregion

        public Result AddUser(User user)
        {
            Result result = ValidateUser(user);

            if (result.Success)
                UserRepository.Insert(user);
            else return result;

            return new Result() { Success = true };
        }

        public static Result ValidateUser(User user)
        {
            if (user.BirthDate > DateTime.Now)
            {
                return new Result() { Success = false, Message = "Birth date can't be a future date." };
            }

            if (user.Email == null) return new Result() { Success = false, Message = "Email is Required" };

            //todo validar se email já existe

            if (!Utils.ValidateEmail.IsValidEmail(user.Email))
            {
                return new Result() { Success = false, Message = "Invalid Email" };
            }

            if (user.SchoolingId < 1 || user.SchoolingId > 4)
            {
                return new Result() { Success = false, Message = "Invalid SchoolingId" };
            }

            return new Result() { Success = true };
        }

        public Result Delete(int id)
        {
            var user = GetUser(id);

            if (user == null) return new Result() { Success = false, Message = "User Not Found" };

            UserRepository.Delete(user);

            return new Result() { Success = true };
        }

        public User? GetUser(int id) { User? user = UserContext.Users?.FirstOrDefault(c => c.Id == id); return user; }

        public List<User> GetUsers() => UserRepository.GetAll();

        public Result UpdateUser(User user)
        {
            Result result = ValidateUser(user);

            if (result.Success)
            {
                UserContext.ChangeTracker.Clear();
                UserRepository.Update(user);
            }
            else return result;

            return new Result() { Success = true };
        }
    }
}
