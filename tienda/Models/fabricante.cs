using System.ComponentModel.DataAnnotations;

namespace tienda.Models
{
    public class fabricante
    {
        public int id { get; set; }
        [Required(ErrorMessage = " Ingrese un {0} Valido")]
        public string? nombre { get; set; }
    }
}
