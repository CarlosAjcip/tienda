using tienda.Models;

namespace tienda.Interface
{
    public interface IRepositorioProducto
    {
        Task Crear(producto producto);
        Task Actualizar(ProductoVM productoVM);
        Task<producto> ObtenerPorId(int id);
        Task Borrar(int id);
        Task<IEnumerable<producto>> Obtener();
    }
}
