using MisPostulacionesApp.Api.DTOs.Requests;
using MisPostulacionesApp.Api.DTOs.Responses;
using MisPostulacionesApp.Api.Utils;

namespace MisPostulacionesApp.Api.Services
{
    public interface IPostulacionService
    {
        Result<ObtenerPostulacionResponse> RegistrarPostulacion(RegistrarPostulacionRequest request);
    }
}
