using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public ReportingStructureService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }


        public ReportingStructure GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                Employee emp = _employeeRepository.GetById(id);
                int numberOfReports = GetChildrenCount(emp.DirectReports);
                ReportingStructure reportingStruct = new ReportingStructure();
                reportingStruct.Employee = emp;
                reportingStruct.NumberOfReports = numberOfReports;
                return reportingStruct;
            }
            return null;
        }

        private int GetChildrenCount(List<Employee> emps)
        {
            if (emps == null || emps.Count == 0)
                return 0;
            int count = emps.Count;
            foreach (Employee emp in emps)
            {
                count += GetChildrenCount(emp.DirectReports);
            }
            return count;
        }

    }
}
