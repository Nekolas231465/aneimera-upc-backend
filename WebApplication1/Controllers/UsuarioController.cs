using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyProjectAPINETCore.Utils;
using System.Security.Principal;
using WebApplication1.Models;
using WebApplication1.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] Usuario usuario)
        {
            String token = await _usuarioService.Login(usuario);
            if (token == "Usuario no existe" || token == "Contraseña incorrecta")
            {
                return StatusCode(StatusCodes.Status401Unauthorized , new { mensaje = token });
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se logueo correctamente", response = token });
        }

        [HttpGet]
        [Route("user_logeado")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> User_logeado()
        {
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
