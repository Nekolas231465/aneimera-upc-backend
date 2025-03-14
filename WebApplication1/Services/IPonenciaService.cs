using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IPonenciaService
    {
        Task<List<Ponencia>> GetAll();
        Task<Ponencia> GetById(int id);
        Task<Ponencia> GetRecent();
        Task Create(Ponencia ponencia, IFormFile file, string uniqueFileName, string ruta);
        Task Update(Ponencia ponencia);
        Task Delete(int id);
        Task ToggleStatus(int id);
    }
}
