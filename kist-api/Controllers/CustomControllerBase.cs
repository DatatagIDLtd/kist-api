using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace kist_api.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        private readonly ILogger<CustomControllerBase> _logger;
        readonly IConfiguration _configuration;


        public CustomControllerBase(ILogger<CustomControllerBase> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


    }
}