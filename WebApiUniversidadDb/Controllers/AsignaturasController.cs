using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.Interfaces;

namespace WebApiUniversidadDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturasController : ControllerBase
    {
        private readonly IAsignaturasAppService asignaturasAppService;
        public AsignaturasController(IAsignaturasAppService asignaturasAppService)
        {
            this.asignaturasAppService = asignaturasAppService;
        }
        
        [HttpGet]
        public async Task<IActionResult> ObtenerAsignaturas()
        {
            List<Asignatura> asignaturas =
                await asignaturasAppService.ObtenerAsignatura();

            return Ok(asignaturas);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObtenerAsignaturaId([FromRoute] int id)
        {
            Asignatura? asignatura = 
                await asignaturasAppService.ObtenerAsignaturaId(id);

            return Ok(asignatura);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarAsignatura(
            [FromBody] Asignatura asignatura)
        {
            var respuesta = await asignaturasAppService.AgregarAsignatura(asignatura);
            return Ok(respuesta);
        }
        [HttpPut]
        public async Task<IActionResult> ActualizarAsignatura(
            [FromBody] Asignatura asignatura)
        {
            await asignaturasAppService.ActualizarAsignatura(asignatura);
            return Ok("Asignatura Actualizada");
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> InactivarAsignatura([FromRoute] int id)
        {
            await asignaturasAppService.InactivarAsignatura(id);

            return Ok("Asignatura Inactivada");
        }
    }
}
