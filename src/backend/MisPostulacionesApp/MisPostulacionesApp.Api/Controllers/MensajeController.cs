using Microsoft.AspNetCore.Mvc;
using MisPostulacionesApp.Api.DTOs.Requests;
using MisPostulacionesApp.Api.Services;

namespace MisPostulacionesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;
        private readonly IPostulacionService _postulacionService;
        public MensajeController(
            INotificacionService notificacionService,
            IPostulacionService postulacionService)
        {
            _notificacionService = notificacionService;
            _postulacionService = postulacionService;
        }
        [HttpPost("enviar")]
        public IActionResult Enviar([FromBody] EnviarMensajeRequest request)
        {
            var result = _postulacionService.ObtenerPostulacion(request.Id);
            var postulacion = result.Value!;
            string mensaje = $@"
                Nueva postulación registrada:
                Título: {postulacion.Titulo}
                Empresa: {postulacion.Empresa}
                Rol: {postulacion.Rol}
                Descripción: {postulacion.Descripcion}
                Tecnologías: {postulacion.Tecnologias}
                Salario: S/.{postulacion.Salario}
                Estado: {postulacion.Estado}
                Modalidad: {postulacion.Modalidad}
                Plataforma: {postulacion.Plataforma}
                Notas: {postulacion.Notas}
                Fecha de postulación: {postulacion.FechaPostulacion:dd/MM/yyyy}
                ";

            _notificacionService.EnviarMensaje(request.NumeroDestino, mensaje);
            return Ok(new {mensaje = "enviado!" });
        }
    }
}
