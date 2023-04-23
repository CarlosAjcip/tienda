using tienda.Models;

namespace tienda.Interface
{
    public interface IRepositorioFabricante
    {
        Task Crear(fabricante fabricante);
        Task<IEnumerable<fabricante>> Obtener();
        Task<fabricante> ObtenerPorId(int id);
        Task Borrar(int id);
        Task Actualizar(fabricante fabricante);
    }
}
