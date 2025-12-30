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

        public Result<ObtenerPostulacionResponse> ObtenerPostulacion(Guid id)
        {
            var postulacion = _postulacionRepository.ObtenerPostulacionPorId(id);
            if(postulacion is null)
            {
                return Result<ObtenerPostulacionResponse>.Failure(Error.NotFound());
            }
            var response = new ObtenerPostulacionResponse(
                postulacion.Id,
                postulacion.Titulo,
                postulacion.Empresa,
                postulacion.Descripcion,
                postulacion.Tecnologias,
                postulacion.Salario,
                postulacion.Estado,
                postulacion.Modalidad,
                postulacion.Plataforma,
                postulacion.FechaPostulacion,
                postulacion.Notas ?? string.Empty,
                postulacion.FechaCreacion);
            return Result<ObtenerPostulacionResponse>.Success(response);
        }

        public Result<Guid> RegistrarPostulacion(RegistrarPostulacionRequest request)
        {
            var id = Guid.CreateVersion7();
            _postulacionRepository.RegistrarPostulacion(new SP_REGISTRAR_POSTULACION(
                id,
                request.Titulo,
                request.Empresa,
                request.Rol,
                request.Descripcion,
                request.Salario,
                request.Tecnologias,
                Estado.Postulado.ToString(),//NOTE: esto puede manejarse desde el front
                request.Modalidad,
                request.Plataforma,
                request.Notas
                ));
            return Result<Guid>.Success(id);
        }
    }
}
