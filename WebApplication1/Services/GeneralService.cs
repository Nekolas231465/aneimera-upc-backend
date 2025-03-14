using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using X.PagedList;

namespace WebApplication1.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly ApplicationDbContext _context;
        public GeneralService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<PaginaDto> GetPaginaAsync(RequestEventosDto requestEventosDto)
        {
            List<EventoDto> eventoDtos = new List<EventoDto>(); 
            if (requestEventosDto.Ponencia == true)
            {
                List<Ponencia> ponencias = await _context.Ponencias.ToListAsync();
                eventoDtos.AddRange(ponencias.Select(ponencia => convertirtoEventoDto(ponencia, "Ponencia")).ToList());
            }
            if (requestEventosDto.Taller == true)
            {
                List<Taller> talleres = await _context.Talleres.ToListAsync();
                eventoDtos.AddRange(talleres.Select(taller => convertirtoEventoDto(taller, "Taller")).ToList());
            }
            if (requestEventosDto.VisitaTecnica == true)
            {
                List<VisitaTecnica> visitaTecnicas = await _context.VisitaTecnicas.ToListAsync();
                eventoDtos.AddRange(visitaTecnicas.Select(visitaTecnica => convertirtoEventoDto(visitaTecnica, "VisitaTecnica")).ToList());
            }
            var pagedList = eventoDtos.ToPagedList(requestEventosDto.page, requestEventosDto.size);

            var paginaDto = new PaginaDto
            {
                Content = pagedList,
                Last = pagedList.IsLastPage,
                TotalElements = pagedList.TotalItemCount
            };

            return paginaDto;
        }

        private EventoDto convertirtoEventoDto(dynamic objeto, String tipo)
        {
            EventoDto evento = new EventoDto();
            evento.Tipo = tipo;

            evento.Titulo = objeto.Titulo;
            evento.Aforo = objeto.Aforo;
            evento.Modalidad = objeto.Modalidad;
            evento.Enlace = objeto.Enlace;
            evento.Fecha = objeto.Fecha;
            evento.Hora = objeto.Hora;
            evento.RutaImagen = objeto.RutaImagen;
            evento.Estado = objeto.Estado;
            if (HasProperty(objeto, "VisitaTecninaId")) evento.VisitaTecninaId = objeto.VisitaTecninaId;
            if (HasProperty(objeto, "TallerId")) evento.TallerId = objeto.TallerId;
            if (HasProperty(objeto, "PonenciaId")) evento.PonenciaId = objeto.PonenciaId;

            return evento;
        }

        private bool HasProperty(dynamic obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }
    }
}
