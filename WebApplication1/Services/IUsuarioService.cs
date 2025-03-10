using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IUsuarioService
    {
        Task<string> Login(Usuario usuario);
    }
}
