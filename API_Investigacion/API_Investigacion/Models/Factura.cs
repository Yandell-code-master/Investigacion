using System.ComponentModel.DataAnnotations;

namespace API_Investigacion.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }

        public DateTime FechaEmision { get; set; }
    }
}
