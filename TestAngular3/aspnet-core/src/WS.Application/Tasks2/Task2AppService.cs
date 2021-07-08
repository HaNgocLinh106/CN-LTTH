using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WS.Tasks.DTO;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using WS.Tasks;
using System.Threading.Tasks;
using Abp.UI;
using WS;
using WS.Employees.DTO;
using WS.Employees;
using WS.Tasks2.DTO;
using WS.Tasks2;
using WS.EmployeeTasks;

namespace WS.Tasks
{
    public class Task2AppService : WSAppServiceBase, ITask2AppService
    {

        private readonly IRepository<Task2> _task2Repository;
        private readonly IRepository<Employee> _employeeRepository;
        public Task2AppService( IRepository<Task2> task2Repository, IRepository<Employee> employeeRepository)
        {
            _task2Repository = task2Repository;
            _employeeRepository = employeeRepository;
        }
        public async Task<List<TaskListDto2>> GetAllTaskDetail(WS.Tasks2.DTO.GetAllTaskInput input)
        {
            try
            {
                var tasks = await _task2Repository
                .GetAll()
                 .Include(t => t.EmployeeTasks)
                 .ThenInclude(x => x.Employee)
                 .WhereIf(input.State.HasValue, task => task.State == input.State.Value)
                .OrderByDescending(task => task.CreationTime)
                .ToListAsync();

                var listTask = tasks.Select(x => new TaskListDto2
                {
                    Id = x.Id,
                    Title = x.Title,
                    State = x.State,
                    EmployeeNames = x.EmployeeTasks.Select(y =>  y.Employee.Name).ToList()

                }).ToList();
                return listTask;
             
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public async Task<TaskListDto2> Create(CreateTaskInput2 input)
        {
            try
            {
                input.TenantId = 1;
                input.State = TaskState.Open;
                var task = ObjectMapper.Map<Task2>(input);
                await _task2Repository.InsertAsync(task);
                await CurrentUnitOfWork.SaveChangesAsync();
                return ObjectMapper.Map<TaskListDto2>(task);
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        public TaskListDto2 GetTask(GetTaskInput2 input)
        {
            var task = _task2Repository.FirstOrDefault(x => x.Id == input.Id);
            var output = ObjectMapper.Map<TaskListDto2>(task);
            return output;
        }
        public async System.Threading.Tasks.Task Update(UpdateTaskInput2 input)
        {
            //C1:
            try
            {
                var task = await _task2Repository.FirstOrDefaultAsync(t => t.Id == input.Id);

                ObjectMapper.Map(input, task);

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public async System.Threading.Tasks.Task AssignTask(AssignTaskInput2 input)
        {
            //C1:
                    var records = _task2Repository
                      .GetAll()
                       .Include(t => t.EmployeeTasks)
                  
                     .FirstOrDefault(y => y.Id == input.Id);
                records.EmployeeTasks.RemoveRange(0, records.EmployeeTasks.Count());
                var a = records.EmployeeTasks;
                await CurrentUnitOfWork.SaveChangesAsync();
                foreach (var employeeId in input.EmployeeIds)
                {
                    var employeeTask = new EmployeeTask();
                    employeeTask.Task2Id = input.Id;
                    employeeTask.EmployeeId = employeeId;
                    

                    records.EmployeeTasks.Add(employeeTask);
                }
                await CurrentUnitOfWork.SaveChangesAsync();
            //var task = GetTask(input.Id);
            //C2:
            //var output = ObjectMapper.Map<Task>(input); 
            //await _taskRepository.UpdateAsync(output);
        }
        public async System.Threading.Tasks.Task Delete(DeleteTaskInput2 input)
        {
            try
            {
                var records =  _task2Repository
               .GetAll()
                .Include(t => t.EmployeeTasks)
                .Where(y => y.Id==input.Id)
               .OrderByDescending(task => task.CreationTime)
               .FirstOrDefault();
                    await _task2Repository.DeleteAsync(records);
            }
            catch (Exception e)
            {
                throw (e);
            }


        }
    }
}
