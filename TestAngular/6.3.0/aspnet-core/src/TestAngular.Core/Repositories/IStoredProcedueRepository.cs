
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestAngular.Repositories
{
    public interface IStoredProcedueRepository
    {
        Task<TR> QueryFirstProc<TR>(string spName, object parameters);

        Task<IEnumerable<R>> QueryProc<R>(string spName);

        Task<int> ExecProc(string spName, object parameters);


        Task<T> ExecuteScalar<T>(string spName, object parameters);

        Task<TR> ExecuteScalarCommand<TR>(string command, object parameters);

    }
}
