using Dapper;
using Microsoft.Data.SqlClient;
using tienda.Interface;
using tienda.Models;

namespace tienda.Servicios
{
    public class RepositorioProducto : IRepositorioProducto
    {
        private readonly string connectionstring;
        public RepositorioProducto(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Actualizar(ProductoVM productoVM)
        {
           using var connection = new SqlConnection(connectionstring);
            await connection.ExecuteAsync(@"update producto
            set nombre = @nombre, precio = @precio, id_fabricante = @id_fabricante, disponible = @disponible
            where id = @id;", productoVM);
        }

        public Task Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Crear(producto producto)
        {
           using var connection = new SqlConnection(connectionstring);

            var id = await connection.QuerySingleAsync<int>(@"insert into producto(nombre,precio,id_fabricante,disponible)
            values(@nombre,@precio,@id_fabricante,@disponible);

            SELECT SCOPE_IDENTITY();", producto);
            producto.id = id;
        }

        public async Task<IEnumerable<producto>> Obtener()
        {
            using var connection = new SqlConnection(connectionstring);
            return await connection.QueryAsync<producto>(@"select  nombre,precio,disponible from producto");

        }

        public async Task<producto> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionstring);
            return await connection.QueryFirstOrDefaultAsync<producto>
            (@"select p.id, p.nombre,p.precio,p.disponible,p.id_fabricante  from producto p
            
            where p.id = @id", new { id });
        }
    }
}
