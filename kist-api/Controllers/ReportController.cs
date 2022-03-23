using kist_api.Model.reports;
using kist_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class ReportController : ControllerBase
    {

        private readonly ILogger<ReportController> _logger;
        readonly IKistService _kistService;

        public ReportController(ILogger<ReportController> logger,  IKistService kistService)
        {
            _logger = logger;
            _kistService = kistService;
        }

        [Route("GetCustomReports")]
        [Authorize]
        public async Task<List<CustomReport>> GetCustomReports()
        {
            var userName = (string)HttpContext.Items["UserName"];

            return await _kistService.GetCustomReports(userName);
        }
    }

}
