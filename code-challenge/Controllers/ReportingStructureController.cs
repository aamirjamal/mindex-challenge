using System;
using Microsoft.AspNetCore.Mvc;
using challenge.Services;

namespace challenge.Controllers
{
    [Route("api/reportingstructure")]
    public class ReportingStructureController : Controller
    {

        private readonly IReportingStructureService _reportingStructService;

        public ReportingStructureController(IReportingStructureService reportingStructService)
        {
            _reportingStructService = reportingStructService;
        }

        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult getReportingStructureById(String id)
        {
            var reportingStruct = _reportingStructService.GetById(id);

            if (reportingStruct == null)
                return NotFound();

            return Ok(reportingStruct);
        }
    }
}