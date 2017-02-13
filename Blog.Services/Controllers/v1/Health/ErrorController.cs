using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Blog.Services.Controllers.v1.Health
{
    /// <summary>
    /// Test that exceptions are thrown correctly
    /// </summary>
    [Route("api/v1/Health/[controller]")]
    public class ErrorController
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public ErrorController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ErrorController");
        }

        /// <summary>
        /// Throw uncaught exception and generate email
        /// </summary>
        [HttpPost]
        //[ServiceFilter(typeof(UncaughtExceptionFilter))]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("Executing Http Get before exception");
            throw new Exception("Yes a great exception");
        }
    }
}