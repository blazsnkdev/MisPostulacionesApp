using Microsoft.AspNetCore.Mvc;
using MisPostulacionesApp.Api.DTOs.Requests;
using MisPostulacionesApp.Api.Services;

namespace MisPostulacionesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class PostulacionController : ControllerBase
    {
        private readonly IPostulacionService _postulacionService;
        public PostulacionController(IPostulacionService postulacionService)
        {
            _postulacionService = postulacionService;
        }
        [HttpPost]
        public IActionResult Create([FromBody] RegistrarPostulacionRequest request)
        {
            var result = _postulacionService.RegistrarPostulacion(request);
            if (!result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result.Error!.Message);
            }
            //return StatusCode(StatusCodes.Status201Created, result.Value);
            return CreatedAtAction(nameof(Get), new { id = result.Value }, new { Id = result.Value });
        }
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var result = _postulacionService.ObtenerPostulacion(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error!.Message);
            }
            return Ok(result.Value);
        }

    }
}
