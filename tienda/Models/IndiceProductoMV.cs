namespace tienda.Models
{
    public class IndiceProductoMV
    {
        public string? tipoFabricnate { get; set; }
        public IEnumerable<producto>? productos { get; set; }

    }
}
