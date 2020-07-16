using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersAndDepartments.Models;
using UsersAndDepartments.Services;

namespace UsersAndDepartments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        [Route("AddUser")]
        [HttpPost]
        public Task<User> AddUser([FromBody] User user)
        {
            if (String.IsNullOrEmpty(user.FIO)) throw new Exception("Не введено ФИО");
            if (user.DepId == 0) throw new Exception("Не введен DepId");
            return _usersService.AddUser(user);
        }

        [Route("GetUsersByDepartment")]
        [HttpGet]
        public Task<List<User>> GetUsersByDepartment([FromQuery] int depId)
        {
            var departments = _usersService.ByDepartmentId(depId);
            return departments;
        }
    }
}