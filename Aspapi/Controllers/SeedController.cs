using Aspapi.Data;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using CsvHelper;
using WorldCities;

namespace Aspapi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase {
        private readonly WorldCitiesContext _db;
        private readonly string _pathName;

        public SeedController(WorldCitiesContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _pathName = Path.Combine(environment.ContentRootPath, "Data/worldcities.csv");
        }

        [HttpPost("Countries")]
        public async Task<IActionResult> ImportCountriesAsync()
        {
            // create a lookup dictionary containing all the countries already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Country> countriesByName = _db.Countries
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture) {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            IEnumerable<WorldCitiesCsv>? records = csv.GetRecords<WorldCitiesCsv>();
            foreach (WorldCitiesCsv record in records) {
                if (countriesByName.ContainsKey(record.country)) {
                    continue;
                }

                Country country = new() {
                    Name = record.country,
                    Iso2 = record.iso2,
                    Iso3 = record.iso3
                };
                await _db.Countries.AddAsync(country);
                countriesByName.Add(record.country, country);
            }

            await _db.SaveChangesAsync();

            return new JsonResult(countriesByName.Count);

        }
    }
}
