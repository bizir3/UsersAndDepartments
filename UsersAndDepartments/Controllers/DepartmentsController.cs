using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsersAndDepartments.Data;
using UsersAndDepartments.Models;

namespace UsersAndDepartments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DBContext _dbContext;
        
        public DepartmentsController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [Route("AddDepartment")]
        [HttpPost]
        public async Task<Department> AddDepartment([FromBody] Department department)
        {
            if (String.IsNullOrEmpty(department.Name)) throw new Exception("Не введено название отдела");
            
            department.DateAdd = DateTime.Now;
            department.DateUpdate = DateTime.Now;
            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync();

            return department;
        }
        
        [Route("UpdateDepartment")]
        [HttpPut]
        public async Task<Department> UpdateDepartment([FromBody] Department department)
        {
            if (String.IsNullOrEmpty(department.Name)) throw new Exception("Не введено название отдела");
            if (department.DepartmentId > 0) throw new Exception("Нет индификатора отдела");

            var departmentDb = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == department.DepartmentId);

            if (departmentDb != null)
            {
                departmentDb.DateUpdate = DateTime.Now;
                departmentDb.Name = department.Name;
            
                _dbContext.Departments.Update(departmentDb);
                await _dbContext.SaveChangesAsync();    
            } else throw new Exception("Отдел не найден");

            return departmentDb;
        }
        
        [Route("DeleteDepartment")]
        [HttpDelete]
        public async Task<bool> DeleteDepartment([FromBody] Department department)
        {
            if (department.DepartmentId > 0) throw new Exception("Нет индификатора отдела");

            var departmentDb = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == department.DepartmentId);
            if (departmentDb != null)
            {
                _dbContext.Departments.Remove(departmentDb);
                await _dbContext.SaveChangesAsync();    
            } else throw new Exception("Отдел не найден");

            return true;
        }
        
        [Route("GetDepartments")]
        [HttpGet]
        public Task<List<Department>> Get()
        {
            var departments= _dbContext.Departments.ToListAsync();
            return departments;
        }
    }
}