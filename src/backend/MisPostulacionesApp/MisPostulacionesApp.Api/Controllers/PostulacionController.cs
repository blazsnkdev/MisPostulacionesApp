using Microsoft.AspNetCore.Mvc;
using MisPostulacionesApp.Api.DTOs.Requests;
using MisPostulacionesApp.Api.Services;
using MisPostulacionesApp.Api.Services.Implements;
using MisPostulacionesApp.Api.Utils;

namespace MisPostulacionesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class PostulacionController : ControllerBase
    {
        private readonly IPostulacionService _postulacionService;
        private readonly IAService _iaService;
        public PostulacionController(
            IPostulacionService postulacionService,
            IAService iaService)
        {
            _postulacionService = postulacionService;
            _iaService = iaService;
        }
        [HttpPost]
        public IActionResult Create([FromBody] RegistrarPostulacionRequest request)
        {
            var result = _postulacionService.RegistrarPostulacion(request);
            if (!result.IsSuccess)
            {
                var responseFail = ApiResponse<object>.Fail(result.Error!.Message);
                return BadRequest(responseFail);
            }
            var responseSuccess = ApiResponse<object>.Success(result.Value);
            return CreatedAtAction(nameof(Get), new { id = result.Value }, responseSuccess);
        }
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var result = _postulacionService.ObtenerPostulacion(id);
            if (!result.IsSuccess)
            {
                var responseFail = ApiResponse<object>.Fail(result.Error!.Message);
                return NotFound(responseFail);
            }
            var responseSuccess = ApiResponse<object>.Success(result.Value!);
            return Ok(responseSuccess);
        }
        [HttpPost("ia/procesar")]
        public async Task<IActionResult> ProcesarIA([FromBody] string texto)
        {
            var resultado = await _iaService.ProcesarTextoAsync(texto);
            return Ok(resultado);
        }

    }
}
