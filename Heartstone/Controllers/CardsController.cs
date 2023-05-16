using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heartstone.Models;

namespace Heartstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<Card> _cardsCollection;

        public CardsController(ILogger<CardsController> logger, IMongoClient mongoClient)
        {
            _logger = logger;
            _mongoClient = mongoClient;
            var database = _mongoClient.GetDatabase("HeartstoneDB"); // Replace "your-database-name" with the actual database name
            _cardsCollection = database.GetCollection<Card>("cards");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards([FromQuery] int page = 1, [FromQuery] int setid = 0, [FromQuery] string artist = "", [FromQuery] int classid = 0, [FromQuery] int rarityid = 0)
        {
            int pageSize = 100;
            int skip = (page - 1) * pageSize;

            var filterBuilder = Builders<Card>.Filter;
            var filter = filterBuilder.Empty; // Start with an empty filter

            if (setid != 0)
                filter &= filterBuilder.Eq(c => c.SetId, setid);

            if (!string.IsNullOrEmpty(artist))
                filter &= filterBuilder.Eq(c => c.Artist, artist);

            if (classid != 0)
                filter &= filterBuilder.Eq(c => c.ClassId, classid);

            if (rarityid != 0)
                filter &= filterBuilder.Eq(c => c.RarityId, rarityid);

            var cards = await _cardsCollection
                .Find(filter)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();

            return cards;
        }
    }
}