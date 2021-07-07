using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Tasks2.DTO;

namespace WS.Tasks2
{
    public interface ITask2AppService
    {
        Task<ListResultDto<TaskListDto2>> GetAllTaskDetail(WS.Tasks2.DTO.GetAllTaskInput input);
        //Task<TaskListDto2> Create(WS.Tasks2.DTO.CreateTaskInput input);
    }
}
