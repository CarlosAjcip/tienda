using Microsoft.AspNetCore.Mvc.Rendering;

namespace tienda.Models
{
    public class ProductoVM : producto
    {
        public IEnumerable<SelectListItem>? TipoFabricanttes { get; set; } 
    }
}
