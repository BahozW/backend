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
    public class SetsController : ControllerBase
    {
        private readonly ILogger<SetsController> _logger;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<Set> _setsCollection;

        public SetsController(ILogger<SetsController> logger, IMongoClient mongoClient)
        {
            _logger = logger;
            _mongoClient = mongoClient;
            var database = _mongoClient.GetDatabase("HeartstoneDB"); // Replace "your-database-name" with the actual database name
            _setsCollection = database.GetCollection<Set>("sets");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Set>>> GetSets()
        {
            var sets = await _setsCollection.Find(_ => true).ToListAsync();
            return sets;
        }
    }
}
