using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersAndDepartments.Data;
using UsersAndDepartments.Models;

namespace UsersAndDepartments.Services
{

    public interface IUsersService
    {
        public Task<List<User>> ByDepartmentId(int departmentId);
        public Task<User> AddUser(User user);
    }
    
    public class UsersService : IUsersService
    {
        private readonly DBContext _dbContext;
        
        public UsersService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<List<User>> ByDepartmentId(int departmentId)
        {
            return _dbContext.Users.Where(d => d.DepId == departmentId).ToListAsync();
        }
        
        public async Task<User> AddUser(User user)
        {
            user.DateAdd = DateTime.Now;
         
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}