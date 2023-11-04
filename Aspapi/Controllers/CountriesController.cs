using Aspapi.DTOs;
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

        // LINQ statement to get country info including population
        [HttpGet("Population/{id}")]
        public CountryPopulation? GetPopulation(int id) {
            /*
             * SELECT ID, NAME, COUNT(City.POPULATION)
             * FROM Countries
             * WHERE Countries.ID = ID
             */
            return
                /*
                 _db.Countries.Where(c => c.Id == id)
                        .Select(c => new CountryPopulation()
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Population = c.Cities.Sum(t => t.Population)
                        }).SingleOrDefault();
                */
                (from country in _db.Countries
                    where country.Id == id
                    select new CountryPopulation()
                    {
                        Id = country.Id,
                        Name = country.Name,
                        Population = country.Cities.Sum(t => t.Population)
                    }).SingleOrDefault();
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
