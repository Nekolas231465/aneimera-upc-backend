using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IGeneralService
    {
        Task<PaginaDto> GetPaginaAsync(RequestEventosDto requestEventosDto);
    }
}
