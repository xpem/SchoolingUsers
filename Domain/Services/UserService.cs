using Models;
using Repositories.Contexts;
using Repositories.UserRepository;

namespace Domain.Services
{
    public class UserService : IUserService
    {

        #region Property
        private IUserRepository<User> UserRepository;
        private UserContext UserContext;
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


            if (!Utils.ValidateEmail.IsValidEmail(user.Email))
            {
                return new Result() { Success = false, Message = "Invalid Email" };
            }

            if (user.SchoolingId < 0 || user.SchoolingId > 2)
            {
                return new Result() { Success = false, Message = "Invalid SchoolingId" };
            }

            return new Result() { Success = true };
        }

        public void Delete(int id)
        {
            var user = GetUser(id);

            if (user == null) return;

            UserRepository.Delete(user);
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
