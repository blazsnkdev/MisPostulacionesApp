using Microsoft.Data.SqlClient;
using System.Data;

namespace MisPostulacionesApp.Api.Data.ConnectionSql
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _cadenaConexion;
        public SqlConnectionFactory(IConfiguration cfg)
        {
            _cadenaConexion = cfg.GetConnectionString("cn1")
                ?? throw new Exception("cn1 no encontrado");
        }
        public IDbConnection CrearConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }

    }
}
