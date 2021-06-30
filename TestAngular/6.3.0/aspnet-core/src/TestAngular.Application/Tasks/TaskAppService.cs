using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAngular.Tasks.DTO;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using Acme.SimpleTaskApp.Tasks;
using System.Threading.Tasks;
using Abp.UI;

namespace TestAngular.Tasks
{
    public class TaskAppService : TestAngularAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Acme.SimpleTaskApp.Tasks.Task> _taskRepository;
        public TaskAppService(IRepository<Acme.SimpleTaskApp.Tasks.Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTaskInput input) {
            var tasks = await _taskRepository
                .GetAll()
                 .Include(t => t.AssignedEmployee)
                .WhereIf(input.State.HasValue, task => task.State == input.State.Value)
                .OrderByDescending(task => task.CreationTime)
                .ToListAsync();
            try
            {
                var dtos = ObjectMapper.Map<List<TaskListDto>>(tasks);

                return new ListResultDto<TaskListDto>(dtos);
            }
            catch(Exception e)
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
                var task = ObjectMapper.Map<Acme.SimpleTaskApp.Tasks.Task>(input);
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
                var task = await _taskRepository.FirstOrDefaultAsync(t => t.Id == input.Id);

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
