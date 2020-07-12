using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersAndDepartments.Data;
using UsersAndDepartments.Models;

namespace UsersAndDepartments.Controllers
{

    public abstract class IDepartmentsService
    {
        public abstract Task<Department> ById(int departmentId);
        public abstract Task<Department> AddDepartment(Department department);
        public abstract Task<Department> UpdateDepartment(Department department);
        public abstract Task<bool> DeleteDepartment(Department department);
        public abstract Task<List<Department>> GetDepartments();
    }
    
    public class DepartmentsService : IDepartmentsService
    {
        private readonly DBContext _dbContext;
        
        public DepartmentsService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public override Task<Department> ById(int departmentId)
        {
            return _dbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }
        
        public override async Task<Department> AddDepartment(Department department)
        {
            department.DateAdd = DateTime.Now;
            department.DateUpdate = DateTime.Now;
         
            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync();

            return department;
        }
        
        public override async Task<Department> UpdateDepartment(Department department)
        {
            department.DateUpdate = DateTime.Now;

            _dbContext.Departments.Update(department);
            await _dbContext.SaveChangesAsync();
            
            return department;
        }
        
        public override async Task<bool> DeleteDepartment(Department department)
        {
            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();    
        
            return true;
        }
        
        public override Task<List<Department>> GetDepartments()
        {
            return _dbContext.Departments.ToListAsync();;
        }
    }
}