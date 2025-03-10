using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService _generalService;
        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }

        [HttpPost]
        [Route("get_eventos")]
        public async Task<ActionResult> Get_eventos([FromBody]RequestEventosDto requestEventosDto)
        {
            PaginaDto eventos = await _generalService.GetPaginaAsync(requestEventosDto);
            return StatusCode(StatusCodes.Status200OK , new { eventos });
        }
    }
}
