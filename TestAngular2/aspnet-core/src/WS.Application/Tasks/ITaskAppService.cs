using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Tasks.DTO;
using WS.Employees.DTO;

namespace WS.Tasks
{
    public interface ITaskAppService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTaskInput input);
        Task<TaskListDto> Create(CreateTaskInput input);
        TaskListDto GetTask(GetTaskInput input);
        System.Threading.Tasks.Task Delete(DeleteTaskInput input);
        System.Threading.Tasks.Task Update(UpdateTaskInput input);
        //List<EmployeeListDetailDto> GetListEmployee();
        }
}
