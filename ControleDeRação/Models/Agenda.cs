using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ControleDeRacao.Models
{
    public class Agenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; }
        public string? HorarioMatutino { get; set; }
        public string? HorarioVespertino { get; set; }
        public string? HorarioNoturno { get; set; }
    }
}
