using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.Interfaces;

namespace WebApiUniversidadDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AulasController : ControllerBase
    {
        private readonly IAulasAppService aulasAppService;
        public AulasController(IAulasAppService aulasAppService)
        {
            this.aulasAppService = aulasAppService;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerAula()
        {
            List<Aula> aulas = await aulasAppService.ObtenerAula();
            return Ok(aulas);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObtenerAulaId([FromRoute] int id)
        {
            Aula? aula = await aulasAppService.ObtenerAulaId(id);
            return Ok(aula);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarAula([FromBody] Aula aula)
        {
            var respuesta = await aulasAppService.AgregarAula(aula);
            return Ok(respuesta);
        }
        [HttpPut]
        public async Task<IActionResult> ActualizarAula([FromBody] Aula aula)
        {
            var respuesta = await aulasAppService.ActualizarAula(aula);
            return Ok(respuesta);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> InactivarAula([FromRoute] int id)
        {
            await aulasAppService.InactivarAula(id);
            return Ok("Aula Inactivada");
        }
    }
}
