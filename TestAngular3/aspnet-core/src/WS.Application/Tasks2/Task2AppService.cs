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
        public async Task<ListResultDto<TaskListDto2>> GetAllTaskDetail(WS.Tasks2.DTO.GetAllTaskInput input)
        {
            try
            {
                var tasks = await _task2Repository
                .GetAll()
                 .Include(t => t.EmployeeTasks)
                 .WhereIf(input.State.HasValue, task => task.State == input.State.Value)
                .OrderByDescending(task => task.CreationTime)
                .ToListAsync();
                var taskDetails = new List<Tasks2.DTO.TaskListDto2>();
                foreach (var task in tasks)
                {
                    var taskDetail = new Tasks2.DTO.TaskListDto2();
                    taskDetail.Id = task.Id;
                    taskDetail.Title = task.Title;
                    taskDetail.State = task.State;
                    taskDetail.EmployeeNames = new List<string>();
                    var listEmployeeId = new List<int>(); // luu tru list cac id cua nhan vien duoc giao task nay

                    foreach (var employees in task.EmployeeTasks)
                    {
                        var employeeId = employees.EmployeeId;
                        listEmployeeId.Add(employeeId);
                    }
                    //lap lay ten cua Employee
                    foreach (var id in listEmployeeId)
                    {
                        var employee = _employeeRepository
                            .GetAll()
                            .Include(e => e.Tasks)
                            .FirstOrDefault(e => e.Id == id);//
                        var employeeName = employee.Name; // lay ten cua employee
                        taskDetail.EmployeeNames.Add(employeeName); // them vao list employee name
                    }
                    taskDetails.Add(taskDetail);
                }
                var dtos = ObjectMapper.Map<List<TaskListDto2>>(taskDetails);

                return new ListResultDto<TaskListDto2>(dtos);
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

            //var task = GetTask(input.Id);
            //C2:
            //var output = ObjectMapper.Map<Task>(input); 
            //await _taskRepository.UpdateAsync(output);
        }
        public async System.Threading.Tasks.Task AssignTask(AssignTaskInput2 input)
        {
            //foreach (var id in input.EmployeeIds)
            //{
            //    var employee = _employeeRepository
            //        .GetAll()
            //        .Include(e => e.Tasks)
            //        .FirstOrDefault(e => e.Id == id);//

            //    if (employee == null)
            //    {
            //        throw new UserFriendlyException("Employee not found.");
            //    }

            //    employee.Tasks.ForEach(t => t.AssignedEmployeeId = null);

            //    await _employeeRepository.DeleteAsync(employee);
            //}

            //C1:
            try
            {
                //foreach(var record in input.EmployeeIds)
                //{
                    var records = _task2Repository
                      .GetAll()
                       .Include(t => t.EmployeeTasks)
                  
                     .FirstOrDefault(y => y.Id == input.Id);
               
                foreach(var employeeId in input.EmployeeIds)
                {
                    var employeeTask = new EmployeeTask();
                    employeeTask.Task2Id = input.Id;
                    employeeTask.EmployeeId = employeeId;
                    records.EmployeeTasks.Add(employeeTask);
                }
                await CurrentUnitOfWork.SaveChangesAsync();
                //.ToListAsync();
                //}
            }
            catch (Exception e)
            {
                throw (e);
            }

            //var task = GetTask(input.Id);
            //C2:
            //var output = ObjectMapper.Map<Task>(input); 
            //await _taskRepository.UpdateAsync(output);
        }
        public async System.Threading.Tasks.Task Delete(DeleteTaskInput2 input)
        {
            try
            {
                var records = await _task2Repository
               .GetAll()
                .Include(t => t.EmployeeTasks)
                .Where(y => y.Id==input.Id)
               .OrderByDescending(task => task.CreationTime)
               .ToListAsync();
                foreach (var item in records)
                {
                    await _task2Repository.DeleteAsync(item);
                }
              
                //var record= new EmployeeTask();
                //do
                //{
                //     record = _employeeTaskRepository
                //        .GetAll()
                //        .FirstOrDefault(e=> e.Task2Id==input.Id);//

                //    if (record == null)
                //    {
                //        //throw new UserFriendlyException("Task not found.");
                //        continue;
                //    }

                //    //employee.Tasks.ForEach(t => t.AssignedEmployeeId = null);

                //    await _employeeTaskRepository.DeleteAsync(record);
                //}
                //while (record != null);

                //var task = _task2Repository.FirstOrDefault(x => x.Id == input.Id);
                //if (task == null)
                //{
                //    throw new UserFriendlyException("No Data Found");
                //}
                //else
                //{
                //    _task2Repository.Delete(task);
                //}

            }
            catch (Exception e)
            {
                throw (e);
            }


        }
    }
}
