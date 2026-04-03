using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.Interfaces;

namespace WebApiUniversidadDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudiantesAppService estudiantesAppService;
        public EstudiantesController(IEstudiantesAppService estudiantesAppService)
        {
            this.estudiantesAppService = estudiantesAppService;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerEstudiantes()
        {
            List<Estudiante> estudiantes = await estudiantesAppService.ObtenerEstudiante();
            return Ok(estudiantes);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObtenerEstudiantesId([FromRoute] int id)
        {
            Estudiante estudiantes = await estudiantesAppService.ObtenerEstudianteId(id);
            return Ok(estudiantes);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarEstudiante([FromBody] Estudiante estudiante)
        {
            var respuesta = await estudiantesAppService.AgregarEstudiante(estudiante);
            return Ok(respuesta);
        }
        [HttpPut]
        public async Task<IActionResult> ActualizarEstudiante([FromBody] Estudiante estudiante)
        {
            var respuesta = await estudiantesAppService.ActualizarEstudiante(estudiante);
            return Ok(respuesta);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> InactivarEstudiante([FromRoute] int id)
        {
            await estudiantesAppService.InactivarEstudiante(id);
            return Ok("Estudiante inactivado");
        }
    }
}
