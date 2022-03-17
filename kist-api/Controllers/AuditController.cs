using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kist_api.Helper.ApiResponse;
using kist_api.Model;
using kist_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace kist_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        readonly IConfiguration _configuration;
        readonly IAuditService _auditService;

        public AuditController(ILogger<AccountController> logger, IConfiguration configuration, IAuditService auditService)
        {
            _logger = logger;
            _configuration = configuration;
            _auditService = auditService;
        }

        [HttpGet("GetAuditById/{id}")]
        public async Task<ApiResponseModel> GetAuditsById(long id)
        {
            var userId = (string)HttpContext.Items["User"];

            var auditData = await _auditService.GetAuditById(id);

            return auditData;
        }


        [HttpGet("GetAllocationAudit")]
        public async Task<ApiResponseModel> GetAllocationAudit()
        {
            var userId = (string)HttpContext.Items["User"];

            var allocationAuditData = await _auditService.GetAllocationAudit();

            return allocationAuditData;
        }

        [HttpPut("PutAllocationAudit")]
        public async Task<ApiResponseModel> PutAllocationAudit ([FromBody] UpdateAuditStatusModel request, long auditId)
        {
            var userId = (string)HttpContext.Items["User"];

            var allocationAuditData = await _auditService.PutAllocationAudit(request,auditId);

            return allocationAuditData;
        }

        [HttpGet("PutAllocationAudit")]
        public async Task<ApiResponseModel> GetRecentAudits()
        {
            var userId = (string)HttpContext.Items["User"];

            var allocationAuditData = await _auditService.GetRecentAudits();

            return allocationAuditData;
        }

    }
}
