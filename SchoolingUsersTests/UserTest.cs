using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using SchoolingUsers.Controllers;
using SchoolingUsers.DTO;

namespace SchoolingUsersTests
{
    public class UserTest
    {
        public Mock<IUserService> mock = new();



        [Fact]
        public void ShouldGetUser()
        {
            List<User> users = new()
            {
                new User() { Name = "Alpha", BirthDate = DateTime.Now.AddYears(-2).Date, Email = "Alpha@gmail.com", LastName = "Alpha", SchoolingId = 1, Id = 1 },
                 new User() { Name = "Beta", BirthDate = DateTime.Now.AddYears(-2).Date, Email = "Beta@gmail.com", LastName = "Beta", SchoolingId = 2, Id = 2 }
        };

            mock.Setup(p => p.GetUsers()).Returns(users);
            UserController userController = new(mock.Object);
            var result = userController.Get();
            Assert.True(users.Equals(result.Value));
        }

        [Fact]
        public void ShouldGetUserById()
        {
            int id = 1;

            User user = new() { Name = "Alpha", BirthDate = DateTime.Now.AddYears(-2).Date, Email = "Alpha@gmail.com", LastName = "Alpha", SchoolingId = 1, Id = 1 };

            mock.Setup(p => p.GetUser(id)).Returns(user);
            UserController userController = new(mock.Object);
            Microsoft.AspNetCore.Mvc.ActionResult<User> result = userController.Get(id);

            Assert.Same(user, ((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public void ShouldAddUser()
        {
            UserDTO userdto = new() { Name = "Alpha", BirthDate = DateTime.Now.AddYears(-2).Date, Email = "Alpha@gmail.com", LastName = "Alpha", SchoolingId = 1 };
            Result resultSuccess = new() { Success = true };

            mock.Setup(p => p.AddUser(It.IsAny<User>())).Returns(resultSuccess);

            UserController userController = new(mock.Object);
            ActionResult result = userController.Post(userdto);
            Assert.Same("User Added", ((OkObjectResult)result).Value);
        }

        [Fact]
        public void CannotCreateAnUserWithABirthDateInAFutureDate()
        {
            User user = new() { Name = "Alpha", BirthDate = DateTime.Now.AddYears(+2).Date, Email = "Alpha@gmail.com", LastName = "Alpha", SchoolingId = 1, Id = 1 };
            Result resultFail = new Result() { Success = false, Message = "Birth date can't be a future date." };

            Result result = UserService.ValidateUser(user);

            Assert.Equal(resultFail.Message, result.Message);
            Assert.Equal(resultFail.Success, result.Success);
        }

        [Fact]
        public void CannotCreateAnUserWithAInvalidEmail()
        {
            User user = new User() { Name = "Alpha", BirthDate = DateTime.Now.AddYears(-2).Date, Email = null, LastName = "Alpha", SchoolingId = 1, Id = 1 };
            Result resultFail = new Result() { Success = false, Message = "Email is Required" };

            Result result = UserService.ValidateUser(user);

            Assert.Equal(resultFail.Message, result.Message);
            Assert.Equal(resultFail.Success, result.Success);

            user = new User() { Name = "Alpha", BirthDate = DateTime.Now.AddYears(-2).Date, Email = "AlphaInvalidEmail", LastName = "Alpha", SchoolingId = 1, Id = 1 };
            resultFail = new Result() { Success = false, Message = "Invalid Email" };

            result = UserService.ValidateUser(user);

            Assert.Equal(resultFail.Message, result.Message);
            Assert.Equal(resultFail.Success, result.Success);
        }

        [Fact]
        public void CannotCreateAnUserWithInvalidSchooling()
        {
            User user = new() { Name = "Alpha", BirthDate = DateTime.Now.AddYears(-2).Date, Email = "Alpha@gmail.com", LastName = "Alpha", SchoolingId = 100, Id = 1 };
            Result resultFail = new() { Success = false, Message = "Invalid SchoolingId" };

            Result result = UserService.ValidateUser(user);

            Assert.Equal(resultFail.Message, result.Message);
            Assert.Equal(resultFail.Success, result.Success);
        }
    }
}