using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ITallerService
    {
        Task<List<Taller>> GetAll();
        Task<Taller> GetById(int id);
        Task Create(Taller taller, IFormFile file, string uniqueFileName, string ruta, IFormFile fileExpositor, string uniqueExpositorFileName, string expositorFileUrl);
        Task Update(Taller taller);
        Task Delete(int id);
        Task ToggleStatus(int id);
    }
}
