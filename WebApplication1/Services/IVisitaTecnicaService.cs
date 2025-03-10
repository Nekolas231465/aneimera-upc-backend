using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IVisitaTecnicaService
    {
        Task<List<VisitaTecnica>> GetAll();
        Task<VisitaTecnica> GetById(int id);
        Task Create(VisitaTecnica visitaTecnica, IFormFile file, string uniqueFileName, string ruta);
        Task Update(VisitaTecnica visitaTecnica);
        Task Delete(int id);
    }
}
