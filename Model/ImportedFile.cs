using ClientsAPI.Infraesctruture;
using System.Text.Json;
using System.Xml.Linq;

namespace ClientsAPI.Model
{
    public class ImportedFile
    {
        public static List<Superusuario> ProcessarBaseRecebida(string filepath)
        {
            List<SuperuserImported> superusersimported = new List<SuperuserImported>();
            try
            {

                using (StreamReader r = new StreamReader(filepath))
                {
                    string json = r.ReadToEnd();
                    superusersimported = JsonSerializer.Deserialize<List<SuperuserImported>>(json);
                }
            }
            catch (JsonException ex)
            {
                throw new JsonException();
            }

            List<Superusuario> superusers = superusersimported
                .Select(i =>
                new Superusuario(i.Name, i.Score, i.IsActive, i.Pais, i.TeamName, i.IsLeader, i.QTDProjetos))
                .ToList();

            return superusers;
        }
    }
}
