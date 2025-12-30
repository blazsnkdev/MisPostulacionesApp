using Microsoft.Data.SqlClient;
using MisPostulacionesApp.Api.Data.ConnectionSql;
using MisPostulacionesApp.Api.Data.Repositories;
using MisPostulacionesApp.Api.Models;
using System.Data;

namespace MisPostulacionesApp.Api.Data.Implements
{
    public class PostulacionRepository : IPostulacionRepository
    {
        private IDbConnectionFactory _dbConnectionFactorySql;
        public PostulacionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactorySql = dbConnectionFactory;
        }

        public SP_OBTENER_POSTULACION_POR_ID? ObtenerPostulacionPorId(Guid id)
        {
            using var conexion = _dbConnectionFactorySql.CrearConexion();
            conexion.Open();

            using var comando = conexion.CreateCommand();
            comando.CommandText = "SP_OBTENER_ULTIMA_POSTULACION";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = id});
            using var lector = comando.ExecuteReader();

            if (!lector.Read())
                return null;

            return new SP_OBTENER_POSTULACION_POR_ID
            {
                Id = lector.GetGuid(lector.GetOrdinal("Id")),
                Titulo = lector.GetString(lector.GetOrdinal("Titulo")),
                Empresa = lector.GetString(lector.GetOrdinal("Empresa")),
                Descripcion = lector.GetString(lector.GetOrdinal("Descripcion")),
                Rol = lector.GetString(lector.GetOrdinal("Rol")),
                Tecnologias = lector.GetString(lector.GetOrdinal("Tecnologias")),
                Salario = lector.GetDecimal(lector.GetOrdinal("Salario")),
                Estado = lector.GetString(lector.GetOrdinal("Estado")),
                Modalidad = lector.GetString(lector.GetOrdinal("Modalidad")),
                Plataforma = lector.GetString(lector.GetOrdinal("Plataforma")),
                Notas = lector.IsDBNull(lector.GetOrdinal("Notas"))
                    ? null
                    : lector.GetString(lector.GetOrdinal("Notas")),
                FechaPostulacion = lector.GetDateTime(lector.GetOrdinal("FechaPostulacion")),
                FechaCreacion = lector.GetDateTime(lector.GetOrdinal("FechaCreacion"))
            };
        }

        public SP_OBTENER_POSTULACION_POR_ID? ObtenerUltimaPostulacion()
        {
            using var conexion = _dbConnectionFactorySql.CrearConexion();
            conexion.Open();

            using var comando = conexion.CreateCommand();
            comando.CommandText = "SP_OBTENER_ULTIMA_POSTULACION";
            comando.CommandType = CommandType.StoredProcedure;

            using var lector = comando.ExecuteReader();

            if (!lector.Read())
                return null;

            return new SP_OBTENER_POSTULACION_POR_ID
            {
                Id = lector.GetGuid(lector.GetOrdinal("Id")),
                Titulo = lector.GetString(lector.GetOrdinal("Titulo")),
                Empresa = lector.GetString(lector.GetOrdinal("Empresa")),
                Descripcion = lector.GetString(lector.GetOrdinal("Descripcion")),
                Rol = lector.GetString(lector.GetOrdinal("Rol")),
                Tecnologias = lector.GetString(lector.GetOrdinal("Tecnologias")),
                Salario = lector.GetDecimal(lector.GetOrdinal("Salario")),
                Estado = lector.GetString(lector.GetOrdinal("Estado")),
                Modalidad = lector.GetString(lector.GetOrdinal("Modalidad")),
                Plataforma = lector.GetString(lector.GetOrdinal("Plataforma")),
                Notas = lector.IsDBNull(lector.GetOrdinal("Notas"))
                    ? null
                    : lector.GetString(lector.GetOrdinal("Notas")),
                FechaPostulacion = lector.GetDateTime(lector.GetOrdinal("FechaPostulacion")),
                FechaCreacion = lector.GetDateTime(lector.GetOrdinal("FechaCreacion"))
            };
        }


        public bool RegistrarPostulacion(SP_REGISTRAR_POSTULACION sp)
        {
            using var conexion = _dbConnectionFactorySql.CrearConexion();
            conexion.Open();
            using var comando = conexion.CreateCommand();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_REGISTRAR_POSTULACION";
            comando.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier) { Value = sp.Id });
            comando.Parameters.Add(new SqlParameter("@TITULO", SqlDbType.NVarChar, 150) { Value = sp.Titulo});
            comando.Parameters.Add(new SqlParameter("@EMPRESA", SqlDbType.NVarChar, 150) { Value = sp.Empresa });
            comando.Parameters.Add(new SqlParameter("@ROL", SqlDbType.NVarChar, 150) { Value = sp.Rol });
            comando.Parameters.Add(new SqlParameter("@DESCRIPCION", SqlDbType.NVarChar, 500) { Value = sp.Descripcion });
            comando.Parameters.Add(new SqlParameter("@TECNOLOGIAS", SqlDbType.NVarChar, 300) { Value = sp.Descripcion});
            comando.Parameters.Add(new SqlParameter("@SALARIO", SqlDbType.Decimal) { Value = sp.Salario });
            comando.Parameters.Add(new SqlParameter("@ESTADO", SqlDbType.NVarChar, 50) { Value = sp.Estado });
            comando.Parameters.Add(new SqlParameter("@MODALIDAD", SqlDbType.NVarChar, 50) { Value = sp.Modalidad });
            comando.Parameters.Add(new SqlParameter("@PLATAFORMA", SqlDbType.NVarChar, 100) { Value = sp.Plataforma });
            comando.Parameters.Add(new SqlParameter("@NOTAS", SqlDbType.NVarChar, 500) { Value = sp.Notas ?? (object)DBNull.Value });
            int filasAfectadas = comando.ExecuteNonQuery();
            return filasAfectadas > 0;
        }
    }
}
