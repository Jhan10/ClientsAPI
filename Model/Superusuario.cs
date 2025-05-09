using ClientsAPI.Infraesctruture;
using ClientsAPI.ViewModel;
using System.Text.Json;
using ClientsAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Model
{
    public class Superusuario
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsActive { get; set; }
        public string Pais { get; set; }
        public string TeamName { get; set; }
        public bool IsLeader { get; set; }
        public int QTDProjetos { get; set; }

        public Superusuario(string Name, int Score, bool IsActive, string Pais, string TeamName, bool IsLeader, int qTDProjetos)
        {
            this.Name = Name ?? "No Name";
            this.Score = Score;
            this.IsActive = IsActive;
            this.Pais = Pais ?? "No Country";
            this.TeamName = TeamName ?? "No Team";
            this.IsLeader = IsLeader;
            QTDProjetos = qTDProjetos;
        }


    }
}
