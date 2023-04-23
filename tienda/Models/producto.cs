namespace tienda.Models
{
    public class producto
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public float precio { get; set; }
        public int id_fabricante { get; set; }
        public int disponible { get; set; }
        public string? TipoFabricantte { get; set; }
    }
}
