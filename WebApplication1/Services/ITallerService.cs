using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ITallerService
    {
        Task<List<Taller>> GetAll();
        Task<Taller> GetById(int id);
        Task Create(Taller taller, IFormFile file, string uniqueFileName, string ruta);
        Task Update(Taller taller);
        Task Delete(int id);
    }
}
