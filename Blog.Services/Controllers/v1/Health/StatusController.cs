using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.Controllers.v1.Health
{
    /// <summary>
    /// Get status of services
    /// </summary>
    [Route("api/v1/Health/[controller]")]
    public class StatusController
    {
        /// <summary>
        /// Get status of services
        /// </summary>
        [HttpGet, HttpPost]
        public string Index()
        {
            return "Running";
        }
    }
}