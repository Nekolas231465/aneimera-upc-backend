using Microsoft.EntityFrameworkCore;
using MyProjectAPINETCore.Utils;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UsuarioService:IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Login(Usuario usuario)
        {
            Usuario usuariologeado = await _context.Usuarios.Where(u=>u.Username.Equals(usuario.Username)).FirstOrDefaultAsync();
            if (usuariologeado != null)
            {
                if (usuariologeado.Password.Equals(usuario.Password))
                {
                    return JwtConfigurator.GetToken(usuariologeado,this._configuration);
                }
                else
                {
                    return "Contraseña incorrecta";
                }
            }
            else
            {
                return "Usuario no existe";
            }
            throw new NotImplementedException();
        }
    }
}
