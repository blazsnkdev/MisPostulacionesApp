using MisPostulacionesApp.Api.Data.Repositories;
using MisPostulacionesApp.Api.DTOs.Requests;
using MisPostulacionesApp.Api.DTOs.Responses;
using MisPostulacionesApp.Api.Models;
using MisPostulacionesApp.Api.Utils;

namespace MisPostulacionesApp.Api.Services.Implements
{
    public class PostulacionService : IPostulacionService
    {
        private readonly IPostulacionRepository _postulacionRepository;

        public PostulacionService(IPostulacionRepository postulacionRepository)
        {
            _postulacionRepository = postulacionRepository;
        }

        public Result<ObtenerPostulacionResponse> RegistrarPostulacion(RegistrarPostulacionRequest request)
        {
            _postulacionRepository.RegistrarPostulacion(new SP_REGISTRAR_POSTULACION(
                request.Titulo,
                request.Empresa,
                request.Rol,
                request.Descripcion,
                request.Salario,
                request.Tecnologias,
                request.Estado,
                request.Modalidad,
                request.Plataforma,
                request.Notas
                ));
            var ultimaPostulacion = _postulacionRepository.ObtenerUltimaPostulacion();
            if(ultimaPostulacion is null)
            {
                return Result<ObtenerPostulacionResponse>.Failure(Error.Validation("no hay postulación"));
            }
            var response = new ObtenerPostulacionResponse(
                ultimaPostulacion.Id,
                ultimaPostulacion.Titulo,
                ultimaPostulacion.Empresa,
                ultimaPostulacion.Descripcion,
                ultimaPostulacion.Tecnologias,
                ultimaPostulacion.Salario,
                ultimaPostulacion.Estado,
                ultimaPostulacion.Modalidad,
                ultimaPostulacion.Plataforma,
                ultimaPostulacion.FechaPostulacion,
                ultimaPostulacion.Notas ?? "",
                ultimaPostulacion.FechaCreacion
                );
            return Result<ObtenerPostulacionResponse>.Success(response);
        }
    }
}
