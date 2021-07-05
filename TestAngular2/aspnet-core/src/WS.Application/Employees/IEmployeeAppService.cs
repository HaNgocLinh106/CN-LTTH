using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Employees.DTO;

namespace WS.Employees
{
    public interface IEmployeeAppService : IApplicationService
    {
       Task<ListResultDto<EmployeeListDto>> GetAll();
        Task<EmployeeListDto> CreateEmployee(CreateEmployeeInput input);
        EmployeeListDto GetEmployee(GetEmployeeInput input);
        Task UpdateEmployee(UpdateEmployeeInput input);
        Task DeleteEmployee(DeleteEmployeeInput input);
    }
}
