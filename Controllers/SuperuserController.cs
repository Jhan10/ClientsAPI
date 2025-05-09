using System;
using System.Reflection;
using System.Text.Json;
using ClientsAPI.Model;
using ClientsAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClientsAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SuperuserController : ControllerBase
    {
        Stopwatch stopwatch = new Stopwatch();

        private readonly ISuperuserRepository _superuserRepository;

        public SuperuserController(ISuperuserRepository superuserRepository)
        {
            _superuserRepository = superuserRepository;
        }

        [HttpGet]
        public IActionResult ShowHighScore()
        {
            stopwatch.Start();
            var superusers = _superuserRepository.Get().Where(su => su.Score > 900 && su.IsActive == true);

            stopwatch.Stop();
            TimeSpan timeElapsed = stopwatch.Elapsed;
            var tempo = timeElapsed.TotalSeconds;
            return Ok(new object[2] { superusers,$"Essa consulta levou {tempo} segundo(s)" });
        }

        [HttpPost]
        public IActionResult Show()
        {
            stopwatch.Stop();

            var superusers = _superuserRepository.Get();

            TimeSpan timeElapsed = stopwatch.Elapsed;
            var tempo = timeElapsed.TotalSeconds;
            return Ok(new object[2] { superusers, $"Essa consulta levou {tempo} segundo(s)" });
        }

        [HttpPost]
        public IActionResult ImportFile([FromForm]ImportedFileViewModel importedFile)
        {
            stopwatch.Start();

            var filePath = Path.Combine("Storage", importedFile.File.FileName);
            try
            {
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                importedFile.File.CopyTo(fileStream);
            }
            catch (FileLoadException e) 
            {
                return BadRequest("O arquivo não pode ser carregado. Certifique-se que é um arquivo JSON. Detalhes: \n" + e.Message);
            }

            
            try
            {
                var suser = ImportedFile.ProcessarBaseRecebida(filePath);
                if (suser.Count > 0)
                {
                    try
                    {
                        _superuserRepository.ImportBase(suser);

                        stopwatch.Stop();
                        TimeSpan timeElapsed = stopwatch.Elapsed;
                        var tempo = timeElapsed.TotalSeconds;
                        return Ok( $"Arquivo Salvo e base processada. {suser.Count} registro(s) salvo(s). \n Essa operação levou {tempo} segundo(s)");
                    }
                    catch (Exception e)
                    {
                        return BadRequest("Os dados não puderam ser gravados. O arquivo foi salvo. Detalhes: \n" + e.Message);
                    }

                }
            }
            catch (Exception e)
            {
                return BadRequest("A base não pode ser processada. Detalhes: \n" + e.Message);

            }

            return BadRequest("O arquivo está vazio.");

        }

        [HttpPost]
        public IActionResult Create(SuperuserViewModel superuserView)
        {
            stopwatch.Start();
            var superuser = new Superusuario(superuserView.Name, superuserView.Score, superuserView.IsActive, superuserView.Pais, superuserView.TeamName, superuserView.IsLeader, superuserView.QTDProjetos);

            _superuserRepository.Add(superuser);

            TimeSpan timeElapsed = stopwatch.Elapsed;
            var tempo = timeElapsed.TotalSeconds;
            return Ok($"Usuário criado! Essa operação levou {tempo} segundo(s)");
        }

        [HttpGet]
        public IActionResult Get(string name)
        {
            stopwatch.Start();

            var superusers = _superuserRepository.Get().Where(s => s.Name==name);

            TimeSpan timeElapsed = stopwatch.Elapsed;
            var tempo = timeElapsed.TotalSeconds;
            return Ok(new object[2] { superusers, $"Essa operação levou {tempo} segundo(s)" });
        }

    }
}
