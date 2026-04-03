using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.AppServices;
using WebApiUniversidadDb.Features.Universidad.Interfaces;

namespace WebApiUniversidadDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesoresAppService profesoresAppService;
        public ProfesoresController(IProfesoresAppService profesoresAppService)
        {
            this.profesoresAppService = profesoresAppService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProfesores()
        {
            List<Profesor> profesores = await profesoresAppService.ObtenerProfesores();
            return Ok(profesores);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObtenerProfesoresId([FromRoute] int id)
        {
            Profesor profesor = await profesoresAppService.ObtenerProfesorId(id);
            return Ok(profesor);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarProfesor([FromBody] Profesor profesor)
        {
            var respuesta = await profesoresAppService.AgregarProfesor(profesor);
            return Ok(respuesta);
        }
        [HttpPut]
        public async Task<IActionResult> ActualizarProfesor([FromBody] Profesor profesor)
        {
            var respuesta = await profesoresAppService.ActualizarProfesor(profesor);
            return Ok(respuesta);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> InactivarProfesor([FromRoute] int id)
        {
            await profesoresAppService.InactivarProfesor(id);
            return Ok("Profesor inactivado");
        }
    }
}
