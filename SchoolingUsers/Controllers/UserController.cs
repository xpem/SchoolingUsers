
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using SchoolingUsers.DTO;


namespace SchoolingUsers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;

        public UserController(IUserService userService)
        {
            UserService = userService;
        }


        [HttpPost(Name = "PostUser")]
        public ActionResult Post([FromBody] UserDTO userDTO)
        {
            try
            {
                if (userDTO == null) return NotFound();
                
                User user = new() { Name = userDTO.Name, LastName = userDTO.LastName, Email = userDTO.Email, BirthDate = userDTO.BirthDate, SchoolingId = userDTO.SchoolingId };

               Result result =  UserService.AddUser(user);

                if (!result.Success)
                {
                    return ValidationProblem(result.Message);
                }

                return Ok("User Added");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut(Name = "PutUser")]
        public ActionResult Put([FromBody] UserDTO userDTO)
        {
            try
            {
                if (userDTO == null) return NotFound();
                if (userDTO.Id == null) return NotFound();

                User? user = UserService.GetUser(userDTO.Id.Value);

                if (user == null) return NotFound();

                user = new() { Id = userDTO.Id.Value, Name = userDTO.Name, LastName = userDTO.LastName, Email = userDTO.Email, BirthDate = userDTO.BirthDate, SchoolingId = userDTO.SchoolingId, HistoricSchooling = user.SchoolingId };

                Result result = UserService.UpdateUser(user);

                if (!result.Success)
                {
                    return ValidationProblem(result.Message);
                }

                return Ok("User Updated");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet(Name = "GetUsers")]
        public ActionResult<IEnumerable<User>> Get()
        {
            return UserService.GetUsers();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> Get(int id)
        {
            var user = UserService.GetUser(id);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpDelete(Name = "DeleteUser")]
        public void Delete(int id)
        {
            try
            {
                UserService.Delete(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
