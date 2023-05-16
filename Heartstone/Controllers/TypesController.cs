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
    public class TypesController : ControllerBase
    {
        private readonly ILogger<TypesController> _logger;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<CardType> _cardTypesCollection;

        public TypesController(ILogger<TypesController> logger, IMongoClient mongoClient)
        {
            _logger = logger;
            _mongoClient = mongoClient;
            var database = _mongoClient.GetDatabase("HeartstoneDB"); 
            _cardTypesCollection = database.GetCollection<CardType>("cardtypes");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardType>>> GetCardTypes()
        {
            var cardTypes = await _cardTypesCollection.Find(_ => true).ToListAsync();
            return cardTypes;
        }
    }
}
