using System.ComponentModel.DataAnnotations;

namespace API_Investigacion.Models
{
    public class Producto
    {
        [Key]
        public string CodigoInterno { get; set; }

        public string? CodigoBarra { get; set; }

        public string Descripcion { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Existencia { get; set; }
    }
}
