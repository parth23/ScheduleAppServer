using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Model;
using LoggerService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace TaskSchedulingProgram.Controllers
{
    [Produces("application/json")]
    [Route("api/Schedule")]
    public class ScheduleController : Controller
    {
        private ILoggerManager _logger;

        public ScheduleController(ILoggerManager logger)
        {
            _logger = logger;
        }
        // GET: api/Schedule
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Schedule/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Schedule employees shift
        /// </summary>
        /// <param name="value"></param>
        /// <returns> list of ScheduleTask type obj: scheduleTasks</returns>
        // POST: api/Schedule
        [HttpPost]
        public IActionResult Post([FromBody]ScheduleTaskRequestParam value)
        {
            try
            {
                List<ScheduleTask> scheduleTasks = new List<ScheduleTask>();

                using (ScheduleRepository objScheduleRepository = new ScheduleRepository())
                {
                    scheduleTasks = objScheduleRepository.ScheduleTask(value);
                }
                return Ok(scheduleTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside POST: api/Schedule action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Schedule/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
