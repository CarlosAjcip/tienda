using Dapper;
using Microsoft.Data.SqlClient;
using tienda.Interface;
using tienda.Models;

namespace tienda.Servicios
{
    public class RepositorioFabricante : IRepositorioFabricante
    {
        private readonly string connectionString;

        public RepositorioFabricante(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Actualizar(fabricante fabricante)
        {
           using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE  fabricante set nombre = @nombre
            WHERE id = @id", fabricante);
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE fabricante WHERE id = @id", new { id });
        }

        public async Task Crear(fabricante fabricante)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO fabricante(nombre)
             VALUES (@nombre)
             SELECT SCOPE_IDENTITY();", fabricante);
        }

        public async Task<IEnumerable<fabricante>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<fabricante>(@"select id,nombre from fabricante order by nombre asc");
        }

        public async Task<IEnumerable<fabricante>> Obteneri()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<fabricante>(@"select id,nombre from fabricante order by nombre asc");
        }


        public async Task<fabricante> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<fabricante>(@"SELECT id, nombre 
            from fabricante where id = @id", new { id });
        }
    }
}
