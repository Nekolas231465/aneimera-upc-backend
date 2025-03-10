using X.PagedList;

namespace WebApplication1.Models
{
    public class PaginaDto
    {
        public IPagedList<EventoDto> Content { get; set; }

        public bool Last { get; set; }
        public int TotalElements { get; set; }
    }
}
