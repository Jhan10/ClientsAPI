using ClientsAPI.Model;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ClientsAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TeamController : ControllerBase
    {
        Stopwatch stopwatch = new Stopwatch();

        private readonly ISuperuserRepository _superuserRepository;

        public TeamController(ISuperuserRepository superuserRepository)
        {
            _superuserRepository = superuserRepository;
        }

        [HttpGet]
        public IActionResult Show()
        {
            stopwatch.Start();

            var teams = _superuserRepository
                .Get()
                .GroupBy(te => te.TeamName);

            Dictionary<string, int> media = new Dictionary<string, int>();
            foreach (var te in teams)
            {
                media.Add(te.Key, te.Count());
            }

            stopwatch.Stop();
            TimeSpan timeElapsed = stopwatch.Elapsed;
            var tempo = timeElapsed.TotalSeconds;
            return Ok(new object[3] { media, teams, $"Essa consulta levou {tempo} segundo(s)" });
        }

        [HttpGet]
        public IActionResult TeamInsights()
        {
            stopwatch.Start();

            var data = _superuserRepository.Get();
            float membrosAtivos = data.Where(d => d.IsActive == true).Count();
            float todosMembros = data.Count();
            float porCent = (membrosAtivos / todosMembros) * 100;

            Dictionary<string, string> insights = new Dictionary<string, string>();
            insights.Add("Total de Membros", data.Count().ToString());
            insights.Add("Total de Líderes", data.Where(d => d.IsLeader == true).Count().ToString());
            insights.Add("Total de projetos concluídos", data.Sum(d => d.QTDProjetos).ToString());
            insights.Add("Membros Ativos", $"{((short)porCent)}%");

            stopwatch.Stop();
            TimeSpan timeElapsed = stopwatch.Elapsed;
            var tempo = timeElapsed.TotalSeconds;
            return Ok(new object[2] { insights, $"Essa consulta levou {tempo} segundo(s)" });
        }
    }
}