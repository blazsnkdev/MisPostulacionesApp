using System.Data;

namespace MisPostulacionesApp.Api.Data.ConnectionSql
{
    public interface IDbConnectionFactory
    {
        IDbConnection CrearConexion();
    }
}
