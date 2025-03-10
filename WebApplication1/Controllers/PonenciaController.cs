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
    public class PonenciaController : ControllerBase
    {
        private readonly IPonenciaService _ponenciaService;

        public PonenciaController(IPonenciaService ponenciaService)
        {
            _ponenciaService = ponenciaService;
        }

        [HttpGet]
        [Route("get_all")]
        public async Task<ActionResult> Get_all()
        {
            List<Ponencia> ponencias = await _ponenciaService.GetAll();
            return StatusCode(StatusCodes.Status200OK, new { ponencias });
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            Ponencia ponencia = await _ponenciaService.GetById(id);
            return StatusCode(StatusCodes.Status200OK, new { ponencia });
        }

        [HttpGet]
        [Route("getRecent")]
        public async Task<ActionResult> GetRecent()
        {
            Ponencia ponencia = await _ponenciaService.GetRecent();
            return StatusCode(StatusCodes.Status200OK, new { ponencia });
        }

        [HttpPost]
        [Route("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Create([FromForm] Ponencia ponencia, IFormFile file)
        {
            List<Ponencia> ponencias = await _ponenciaService.GetAll();

            if (file == null || file.Length == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al enviar archivo." });
            }

            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string fileUrl = Url.Content("~/Uploads/" + uniqueFileName);
                await _ponenciaService.Create(ponencia, file, uniqueFileName, fileUrl);
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
        public async Task<ActionResult> Update([FromBody] Ponencia ponencia)
        {
            try
            {
                await _ponenciaService.Update(ponencia);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se edito correctamente" });
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
                await _ponenciaService.Delete(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se elimino correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }
    }
}
