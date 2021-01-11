using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            // Also add validation for not adding duplicates or no employee with passed id
            var alreadyExists = GetByEmpId(compensation.EmployeeId);
            var emp = _employeeRepository.GetById(compensation.EmployeeId);
            if (compensation != null && alreadyExists == null && emp != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetByEmpId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var compensation = _compensationRepository.GetByEmpId(id);
                if (compensation == null) return null;
                var emp = _employeeRepository.GetById(id);
                compensation.Employee = emp;
                return compensation;
            }

            return null;
        }
    }
}
