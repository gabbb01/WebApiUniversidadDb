using Microsoft.AspNetCore.Mvc;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.Interfaces;

namespace WebApiUniversidadDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculasAppService matriculasAppService;

        public MatriculasController(IMatriculasAppService matriculasAppService)
        {
            this.matriculasAppService = matriculasAppService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMatriculas()
        {
            var matriculas = await matriculasAppService.ObtenerMatriculas();
            return Ok(matriculas);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObtenerMatriculaId([FromRoute] int id)
        {
            var matricula = await matriculasAppService.ObtenerMatriculaId(id);
            return Ok(matricula);
        }

        [HttpGet]
        [Route("estudiante/{estudianteId}")]
        public async Task<IActionResult> ObtenerPorEstudiante([FromRoute] int estudianteId)
        {
            var matriculas = await matriculasAppService.ObtenerMatriculasPorEstudiante(estudianteId);
            return Ok(matriculas);
        }

        [HttpGet]
        [Route("promedios")]
        public async Task<IActionResult> ObtenerPromediosPorEstudiante()
        {
            var promedios = await matriculasAppService.ObtenerPromediosPorEstudiante();
            return Ok(promedios);
        }

        [HttpGet]
        [Route("estadisticas")]
        public async Task<IActionResult> ObtenerEstadisticas()
        {
            var estadisticas = await matriculasAppService.ObtenerEstadisticasGenerales();
            return Ok(estadisticas);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarMatricula([FromBody] Matricula matricula)
        {
            await matriculasAppService.AgregarMatricula(matricula);
            return Ok(new { success = true, message = "Matrícula registrada exitosamente" });
        }

        [HttpPut]
        [Route("{id}/nota")]
        public async Task<IActionResult> ActualizarNota([FromRoute] int id, [FromBody] decimal nota)
        {
            await matriculasAppService.ActualizarNota(id, nota);
            return Ok(new { success = true, message = "Nota actualizada exitosamente" });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> InactivarMatricula([FromRoute] int id)
        {
            await matriculasAppService.InactivarMatricula(id);
            return Ok(new { success = true, message = "Matrícula inactivada" });
        }
    }
}
