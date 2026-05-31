using System.ComponentModel.DataAnnotations;

namespace API_Investigacion.Models
{
    public class Cliente
    {
        [Key]
        public int id { get; set; }
        public string Nombre { get; set; }
    }
}
