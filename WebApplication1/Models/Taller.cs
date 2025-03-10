using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Taller
    {
        [Key]
        public int TallerId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int Aforo { get; set; }
        public string Modalidad { get; set; }
        public string Enlace { get; set; }
        public string RutaImagen { get; set; }

        //Expositor
        public string ExpositorNombre { get; set; }
        public string ExpositorRol { get; set; }
        public string ExpositorRutaImagen { get; set; }
    }
}
