using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersAndDepartments.Data;
using UsersAndDepartments.Models;

namespace UsersAndDepartments.Controllers
{

    public abstract class IUsersService
    {
        public abstract Task<List<User>> ByDepartmentId(int departmentId);
        public abstract Task<User> AddUser(User user);
    }
    
    public class UsersService : IUsersService
    {
        private readonly DBContext _dbContext;
        
        public UsersService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public override Task<List<User>> ByDepartmentId(int departmentId)
        {
            return _dbContext.Users.Where(d => d.DepId == departmentId).ToListAsync();
        }
        
        public override async Task<User> AddUser(User user)
        {
            user.DateAdd = DateTime.Now;
         
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}