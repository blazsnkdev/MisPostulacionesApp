using MisPostulacionesApp.Api.Models;

namespace MisPostulacionesApp.Api.Data.Repositories
{
    public interface IPostulacionRepository
    {
        bool RegistrarPostulacion(SP_REGISTRAR_POSTULACION model);
        SP_OBTENER_POSTULACION_POR_ID? ObtenerUltimaPostulacion();
        SP_OBTENER_POSTULACION_POR_ID? ObtenerPostulacionPorId(Guid id);
    }
}
