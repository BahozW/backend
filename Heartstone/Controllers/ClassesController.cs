using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heartstone.Models;

namespace Heartstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly ILogger<ClassesController> _logger;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<Class> _classesCollection;

        public ClassesController(ILogger<ClassesController> logger, IMongoClient mongoClient)
        {
            _logger = logger;
            _mongoClient = mongoClient;
            var database = _mongoClient.GetDatabase("HeartstoneDB"); // Replace "your-database-name" with the actual database name
            _classesCollection = database.GetCollection<Class>("classes");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            var classes = await _classesCollection.Find(_ => true).ToListAsync();
            return classes;
        }
    }
}
