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
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class TallerController : ControllerBase
    {
        private readonly ITallerService _tallerService;

        public TallerController(ITallerService tallerService)
        {
            _tallerService = tallerService;
        }

        [HttpGet]
        [Route("get_all")]
        public async Task<ActionResult> Get_all()
        {
            List<Taller> talleres = await _tallerService.GetAll();
            return StatusCode(StatusCodes.Status200OK, new { talleres });
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            Taller taller = await _tallerService.GetById(id);
            return StatusCode(StatusCodes.Status200OK, new { taller });
        }

        [HttpPost]
        [Route("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Create([FromForm] Taller taller, IFormFile file, IFormFile fileExpositor)
        {
            if (file == null || file.Length == 0 || fileExpositor == null || fileExpositor.Length == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al enviar archivo." });
            }

            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string fileUrl = Url.Content("~/Uploads/" + uniqueFileName);

                string uniqueExpositorFileName = Guid.NewGuid().ToString() + "_" + fileExpositor.FileName;
                string expositorFileUrl = Url.Content("~/Uploads/" + uniqueExpositorFileName);

                await _tallerService.Create(taller, file, uniqueFileName, fileUrl, fileExpositor, uniqueExpositorFileName, expositorFileUrl);
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
        public async Task<ActionResult> Update([FromBody] Taller taller)
        {
            try
            {
                await _tallerService.Update(taller);
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
                await _tallerService.ToggleStatus(id);
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
                await _tallerService.Delete(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se elimino correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }
    }
}
