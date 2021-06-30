using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAngular.Employees.DTO;

namespace TestAngular.Employees
{
    public interface IEmployeeAppService : IApplicationService
    {
        public Task<ListResultDto<EmployeeListDto>> GetAll();
        Task CreateEmployee(CreateEmployeeInput input);
        EmployeeListDto GetEmployee(GetEmployeeInput input);
        Task UpdateEmployee(UpdateEmployeeInput input);
        Task DeleteEmployee(DeleteEmployeeInput input);
        void GetAll1();
    }
}
