using Microsoft.AspNetCore.Mvc;
using MisPostulacionesApp.Api.DTOs.Requests;
using MisPostulacionesApp.Api.Services;
using MisPostulacionesApp.Api.Utils;

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

    }
}
