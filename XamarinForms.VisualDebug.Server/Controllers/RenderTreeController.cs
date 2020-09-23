using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VisualDebug.Models;

namespace XamarinForms.VisualDebug.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RenderTreeController : ControllerBase
    {
        private readonly ILogger<RenderTreeController> _logger;

        public RenderTreeController(ILogger<RenderTreeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<RenderRepresentation>> GetAsync()
        {
            return new List<RenderRepresentation>() { new RenderRepresentation() };
        }

        [HttpPost]
        public Task PostAsync(RenderRepresentation renderRepresentation)
        {
            return Task.CompletedTask;
        }
    }
}
