using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Model
{
    public class SuperuserImported
    {
        [Key]
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsActive { get; set; }
        public string Pais { get; set; }
        public string TeamName { get; set; }
        public bool IsLeader { get; set; }
        public int QTDProjetos { get; set; }
    }

}
