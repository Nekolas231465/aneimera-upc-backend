using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class VisitaTecnicaController : ControllerBase
    {
        private readonly IVisitaTecnicaService _visitaTecnicaService;
        public VisitaTecnicaController(IVisitaTecnicaService visitaTecnicaService)
        {
            _visitaTecnicaService = visitaTecnicaService;
        }

        [HttpGet]
        [Route("get_all")]
        public async Task<ActionResult> Get_all()
        {
            List<VisitaTecnica> visitaTecnicas = await _visitaTecnicaService.GetAll();
            return StatusCode(StatusCodes.Status200OK, new { visitaTecnicas });
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            VisitaTecnica visitaTecnica = await _visitaTecnicaService.GetById(id);
            return StatusCode(StatusCodes.Status200OK, new { visitaTecnica });
        }

        [HttpPost]
        [Route("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Create([FromForm] VisitaTecnica visitaTecnica, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al enviar archivo." });
            }

            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string fileUrl = Url.Content("~/Uploads/" + uniqueFileName);
                await _visitaTecnicaService.Create(visitaTecnica, file, uniqueFileName, fileUrl);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se creo correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }

        }

        [HttpPut]
        [Route("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Update([FromBody] VisitaTecnica visitaTecnica)
        {
            try
            {
                await _visitaTecnicaService.Update(visitaTecnica);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se edito correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }

        }
        
        [HttpPatch]
        [Route("updateStatus/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdateStatus(int id)
        {
            try
            {
                await _visitaTecnicaService.ToggleStatus(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _visitaTecnicaService.Delete(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se elimino correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }
    }
}
