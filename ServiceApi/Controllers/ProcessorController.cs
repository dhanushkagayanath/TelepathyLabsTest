using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Services.Interface;

namespace ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorController : ControllerBase
    {
        private readonly ILogger<ProcessorController> _logger;
        private IBTService _btService;

        public ProcessorController(IBTService btService, ILogger<ProcessorController> logger)
        {
            this._btService = btService;
            this._logger = logger;
        }

        [HttpPost]
        [Route("ProcessString")]
        public AppResponse<CalResult> ProcessString([FromBody] AppRequest<string> request)
        {
            AppResponse<CalResult> response = new AppResponse<CalResult>();
            CalResult result = _btService.ProcessString(request.Content);
            response.Content = result;
            response.Success = true;
            return response;
        }
    }
}
