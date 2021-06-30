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
using TestAngular.EntityFrameworkCore;

namespace TestAngular.Tasks
{
    public class TaskAppService : TestAngularAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Acme.SimpleTaskApp.Tasks.Task> _taskRepository;
        public TaskAppService(IRepository<Acme.SimpleTaskApp.Tasks.Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTaskInput input)
        {
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
            catch (Exception e)
            {
                throw (e);
            }
        }
        public void GetAll1()
        {
            var a = 1;

            //var pageObject = (from t in _context.Tasks
            //                  join emp in _context.Employees on t.AssignedEmployeeId equals emp.Id

            //                  select emp.Id)
            //     .SingleOrDefault();


            var result = _taskRepository.GetAll()
                            .Where(s => s.AssignedEmployeeId.HasValue)
                            .Select(e => new
                            {
                                EmployeeId = e.AssignedEmployeeId,
                                EmployeeName = e.AssignedEmployee.Name,
                                e.State
                            })
                            .GroupBy(l => new { l.EmployeeId, l.EmployeeName })
                            .Select(cl => new
                            {
                                EmployeeId = cl.Key.EmployeeId,
                                EmployeeName = cl.Key.EmployeeName,
                                TaskPending = cl.Where(x => x.State == TaskState.Open).Count().ToString(),
                                TaskComplit = cl.Where(y => y.State == TaskState.Completed).Count().ToString(),
                            }).ToList();
            var acb = 1;

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
