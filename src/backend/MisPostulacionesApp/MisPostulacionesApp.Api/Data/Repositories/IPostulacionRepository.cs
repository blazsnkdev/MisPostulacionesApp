using MisPostulacionesApp.Api.Models;

namespace MisPostulacionesApp.Api.Data.Repositories
{
    public interface IPostulacionRepository
    {
        string RegistrarPostulacion(SP_REGISTRAR_POSTULACION model);
        SP_OBTENER_ULTIMA_POSTULACION? ObtenerUltimaPostulacion();
    }
}
