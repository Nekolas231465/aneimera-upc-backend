using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Ponencia
    {
        [Key]
        public int PonenciaId { get; set; }
        public string Titulo { get; set; }
        public string MisionObjetivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int Aforo { get; set; }
        public string Modalidad { get; set; }
        public string Enlace { get; set; }
        public string RutaImagen { get; set; }
    }
}
