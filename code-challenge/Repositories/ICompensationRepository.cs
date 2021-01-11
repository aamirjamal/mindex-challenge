using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetByEmpId(String empId);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}