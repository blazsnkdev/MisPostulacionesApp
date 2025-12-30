using MisPostulacionesApp.Api.DTOs.Requests;
using MisPostulacionesApp.Api.DTOs.Responses;
using MisPostulacionesApp.Api.Utils;

namespace MisPostulacionesApp.Api.Services
{
    public interface IPostulacionService
    {
        Result<Guid> RegistrarPostulacion(RegistrarPostulacionRequest request);
        Result<ObtenerPostulacionResponse> ObtenerPostulacion(Guid id);
    }
}
