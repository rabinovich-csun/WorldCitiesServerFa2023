using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldCities;

namespace Aspapi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly WorldCitiesContext _db;

        public CountriesController(WorldCitiesContext db)
        {
            _db = db;
        }

        // GET: api/<CountriesController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Country> Get()
        {
            return _db.Countries.ToList();
        }

        // GET api/<CountriesController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<CountriesController>
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/<CountriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<CountriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
