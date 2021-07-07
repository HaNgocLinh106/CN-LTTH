using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Employees.DTO;
using WS;
using WS.Employees;
using Abp.UI;
using WS.Tasks;
using WS.EmployeeTasks;

namespace WS.Employees
{
    public class EmployeeAppService : WSAppServiceBase, IEmployeeAppService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<WS.Tasks.Task> _taskRepository;

        private readonly ITaskAppService _taskService;
        public EmployeeAppService(IRepository<Employee> employeeRepository, IRepository<WS.Tasks.Task> taskRepository, ITaskAppService taskService)
        {
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepository;
            _taskService = taskService;
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

        public async Task<EmployeeListDto> CreateEmployee(CreateEmployeeInput input)
        {
            try
            {
                var employee = ObjectMapper.Map<Employee>(input);
                await _employeeRepository.InsertAsync(employee);
                await CurrentUnitOfWork.SaveChangesAsync();
                return ObjectMapper.Map<EmployeeListDto>(employee);
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        public async Task<List<EmployeeListDetailDto>> GetListEmployee()
        {

            var employees = await _employeeRepository
                .GetAll()
                .Include(e => e.Tasks)
                .ToListAsync();
            return employees.Select(e => new EmployeeListDetailDto
            {
                Id = e.Id,
                BirthDate = e.BirthDate,
                Age = e.Age,
                Name = e.Name,
                TaskPending = e.Tasks.Count(t => t.State == TaskState.Open),
                TaskComplete = e.Tasks.Count(t => t.State == TaskState.Completed)
            }).ToList();
            //C2:
            //return await (
            //    from e in _employeeRepository.GetAll()
            //    join t in _taskRepository.GetAll() on e.Id equals t.AssignedEmployeeId into et
            //    from ee in et.DefaultIfEmpty()
            //    group ee by new { e.Id, e.BirthDate, e.Name } into g
            //    select new EmployeeListDetailDto
            //    {
            //        Id = g.Key.Id,
            //        BirthDate = g.Key.BirthDate,
            //        Name = g.Key.Name,
            //        TaskPending = g.Count(i => i.State == TaskState.Open),
            //        TaskComplete = g.Count(i => i.State == TaskState.Completed)
            //    }).ToListAsync();
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
                foreach (var id in input.EmployeeIds)
                {
                    var employee = _employeeRepository
                        .GetAll()
                        .Include(e => e.Tasks)
                        .FirstOrDefault(e => e.Id == id);//

                    if (employee == null)
                    {
                        throw new UserFriendlyException("Employee not found.");
                    }

                    employee.Tasks.ForEach(t => t.AssignedEmployeeId = null);

                    await _employeeRepository.DeleteAsync(employee);
                }

            }
            catch (Exception e)
            {
                throw (e);
            }

        }
    }
}
