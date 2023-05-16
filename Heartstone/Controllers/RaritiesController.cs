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
    public class RaritiesController : ControllerBase
   {
    private readonly ILogger<RaritiesController> _logger;
    private readonly IMongoClient _mongoClient;
    private readonly IMongoCollection<Rarity> _raritiesCollection;

    public RaritiesController(ILogger<RaritiesController> logger, IMongoClient mongoClient)
    {
        _logger = logger;
        _mongoClient = mongoClient;
        var database = _mongoClient.GetDatabase("your-database-name");
        _raritiesCollection = database.GetCollection<Rarity>("rarities");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rarity>>> GetRarities()
    {
        var rarities = await _raritiesCollection.Find(_ => true).ToListAsync();
        return rarities;
    }
}
}

