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

namespace WS.Tasks
{
    public class TaskAppService : WSAppServiceBase, ITaskAppService
    {
        private readonly IRepository<WS.Tasks.Task> _taskRepository;
        private readonly IRepository<Task2> _task2Repository;
        private readonly IRepository<Employee> _employeeRepository;
        public TaskAppService(IRepository<WS.Tasks.Task> taskRepository, IRepository<Task2> task2Repository, IRepository<Employee> employeeRepository)
        {
            _taskRepository = taskRepository;
            _task2Repository = task2Repository;
            _employeeRepository = employeeRepository;
        }
        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTaskInput input)
        {
 
            try
            {
                var tasks = await _taskRepository
                .GetAll()
                 .Include(t => t.AssignedEmployee)
                .WhereIf(input.State.HasValue, task => task.State == input.State.Value)
                .OrderByDescending(task => task.CreationTime)
                .ToListAsync();
                var dtos = ObjectMapper.Map<List<TaskListDto>>(tasks);

                return new ListResultDto<TaskListDto>(dtos);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
      
        public async Task<TaskListDto> Create(CreateTaskInput input)
        {
            try
            {
                input.TenantId = 1;
                input.State = TaskState.Completed;
                var task = ObjectMapper.Map<WS.Tasks.Task>(input);
                await _taskRepository.InsertAsync(task);
                await CurrentUnitOfWork.SaveChangesAsync();
                return ObjectMapper.Map<TaskListDto>(task);
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        public TaskListDto GetTask(GetTaskInput input)
        {
            var task = _taskRepository.FirstOrDefault(x => x.Id == input.Id);
            var output = ObjectMapper.Map<TaskListDto>(task);
            return output;
        }
        public async System.Threading.Tasks.Task Update(UpdateTaskInput input)
        {
            //C1:
            try
            {
                var task = await  _taskRepository.FirstOrDefaultAsync(t => t.Id == input.Id);

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

        public async System.Threading.Tasks.Task Delete(DeleteTaskInput input)
        {
            try
            {
                var task = _taskRepository.FirstOrDefault(x => x.Id == input.Id);
                if (task == null)
                {
                    throw new UserFriendlyException("No Data Found");
                }
                else
                {
                    _taskRepository.Delete(task);
                }
            }
            catch (Exception e)
            {
                throw (e);
            }


        }
    }
}
