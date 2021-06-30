using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAngular.Employees.DTO;
using TestAngular.EntityFrameworkCore;
using TestAngular.Repositories;

namespace TestAngular.Employees
{
    public class EmployeeAppService : TestAngularAppServiceBase, IEmployeeAppService
    {
        private readonly IRepository<Employee> _employeeRepository;
      
        public EmployeeAppService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<ListResultDto<EmployeeListDto>> GetAll()
        {
            try
            {
                var employees = await _employeeRepository.GetAll().ToListAsync();
                var dtos = ObjectMapper.Map<List<EmployeeListDto>>(employees);

                return new ListResultDto<EmployeeListDto>(dtos);
            }
            catch (Exception e)
            {
                throw (e);
            }
          
        }

        //public void GetAll1()
        //{
        //    var a = 1;

        //    var pageObject = (from t in _context.Tasks
        //                      join emp in _context.Employees on t.AssignedEmployeeId equals emp.Id
                          
        //                      select emp.Id)
        //         .SingleOrDefault();
        //}

        public async System.Threading.Tasks.Task CreateEmployee(CreateEmployeeInput input)
        {
            try {
                var employee = ObjectMapper.Map<Employee>(input);
                await _employeeRepository.InsertAsync(employee);
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        public EmployeeListDto GetEmployee(GetEmployeeInput input)
        {
            try
            {
                var employee = _employeeRepository.FirstOrDefault(x => x.Id == input.Id);
                var output = ObjectMapper.Map<EmployeeListDto>(employee);
                return output;
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        public async System.Threading.Tasks.Task UpdateEmployee(UpdateEmployeeInput input)
        {
            try
            {
                var employee = await _employeeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
                ObjectMapper.Map(input, employee);
            }
            catch (Exception e)
            {
                throw (e);
            }

           
        }
        public async System.Threading.Tasks.Task DeleteEmployee(DeleteEmployeeInput input)
        {
            try
            {
                var employee = await _employeeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
                await _employeeRepository.DeleteAsync(employee);
            }
            catch (Exception e)
            {
                throw (e);
            }
           
        }
    }
}
