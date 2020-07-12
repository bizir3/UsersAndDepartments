using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersAndDepartments.Models;

namespace UsersAndDepartments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;
        
        public DepartmentsController(IDepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }
        
        [Route("AddDepartment")]
        [HttpPost]
        public Task<Department> AddDepartment([FromBody] Department department)
        {
            if (String.IsNullOrEmpty(department.Name)) throw new Exception("Не введено название отдела");
            return _departmentsService.AddDepartment(department);
        }
        
        [Route("UpdateDepartment")]
        [HttpPut]
        public async Task<Department> UpdateDepartment([FromBody] Department department)
        {
            if (String.IsNullOrEmpty(department.Name)) throw new Exception("Не введено название отдела");
            if (department.DepartmentId > 0) throw new Exception("Нет индификатора отдела");

            var departmentDb = await _departmentsService.ById(department.DepartmentId);
            if (departmentDb != null)
            {
                departmentDb.Name = department.Name;
                await _departmentsService.UpdateDepartment(departmentDb);
            } else throw new Exception("Отдел не найден");

            return departmentDb;
        }
        
        [Route("DeleteDepartment")]
        [HttpDelete]
        public async Task<bool> DeleteDepartment([FromBody] int departmentId)
        {
            if (departmentId > 0) throw new Exception("Нет индификатора отдела");

            var departmentDb = await _departmentsService.ById(departmentId);
            if (departmentDb != null)
            {
                await _departmentsService.DeleteDepartment(departmentDb);
            } else throw new Exception("Отдел не найден");

            return true;
        }
        
        [Route("GetDepartments")]
        [HttpGet]
        public Task<List<Department>> GetDepartments()
        {
            var departments= _departmentsService.GetDepartments();
            return departments;
        }
    }
}