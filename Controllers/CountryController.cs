using ClientsAPI.Model;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ClientsAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CountryController : ControllerBase
    {
        Stopwatch stopwatch = new Stopwatch();

        private readonly ISuperuserRepository _superuserRepository;

        public CountryController(ISuperuserRepository superuserRepository)
        {
            _superuserRepository = superuserRepository;
        }

        [HttpGet]
        public IActionResult ShowSuperusersByCountry()
        {
            stopwatch.Start();

            var superusersByCountry = _superuserRepository
                .Get()
                .GroupBy(su => su.Pais);

            stopwatch.Stop();
            TimeSpan timeElapsed = stopwatch.Elapsed;
            var tempo = timeElapsed.TotalSeconds;
            return Ok(new object[2] { superusersByCountry, $"Essa consulta levou {tempo} segundo(s)" });
        }


        [HttpGet]
        public IActionResult ShowTopCountrySuperusers()
        {
            stopwatch.Start();

            var superusersByCountry = _superuserRepository
                .Get()
                .GroupBy(su => su.Pais)
                .OrderByDescending(c => c.Count())
                .Take(2);
            Dictionary<string,int> media = new Dictionary<string, int>();
            foreach (var su in superusersByCountry)
            {
                media.Add(su.Key, su.Count());
            }

            stopwatch.Stop();
            TimeSpan timeElapsed = stopwatch.Elapsed;
            var tempo = ((short)timeElapsed.TotalSeconds);
            return Ok(new object[3] { media, superusersByCountry, $"Essa consulta levou {tempo} segundo(s)" });
        }
    }
}
