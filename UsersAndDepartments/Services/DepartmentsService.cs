using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersAndDepartments.Data;
using UsersAndDepartments.Models;

namespace UsersAndDepartments.Services
{

    public interface IDepartmentsService
    {
        public Task<Department> ById(int departmentId);
        public Task<Department> AddDepartment(Department department);
        public Task<Department> UpdateDepartment(Department department);
        public Task<bool> DeleteDepartment(Department department);
        public Task<List<Department>> GetDepartments();
    }
    
    public class DepartmentsService : IDepartmentsService
    {
        private readonly DBContext _dbContext;
        
        public DepartmentsService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<Department> ById(int departmentId)
        {
            return _dbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }
        
        public async Task<Department> AddDepartment(Department department)
        {
            department.DateAdd = DateTime.Now;
            department.DateUpdate = DateTime.Now;
         
            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync();

            return department;
        }
        
        public async Task<Department> UpdateDepartment(Department department)
        {
            department.DateUpdate = DateTime.Now;

            _dbContext.Departments.Update(department);
            await _dbContext.SaveChangesAsync();
            
            return department;
        }
        
        public async Task<bool> DeleteDepartment(Department department)
        {
            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();    
        
            return true;
        }
        
        public Task<List<Department>> GetDepartments()
        {
            return _dbContext.Departments.ToListAsync();;
        }
    }
}