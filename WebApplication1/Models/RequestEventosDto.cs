namespace WebApplication1.Models
{
    public class RequestEventosDto
    {
        public bool Ponencia { get; set; }
        public bool Taller { get; set; }
        public bool VisitaTecnica { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
