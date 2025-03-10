namespace WebApplication1.Models
{
    public class EventoDto
    {
        public string Titulo { get; set; }
        public int Aforo { get; set; }
        public string Modalidad { get; set; }
        public string Tipo { get; set; }
        public string Enlace { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string RutaImagen { get; set; }
        public int? VisitaTecninaId { get; set; }
        public int? TallerId { get; set; }
        public int? PonenciaId { get; set; }
    }
}
