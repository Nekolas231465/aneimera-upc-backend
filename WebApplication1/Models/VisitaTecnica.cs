using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class VisitaTecnica
    {
        [Key]
        public int VisitaTecninaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int Aforo { get; set; }
        public string Modalidad { get; set; }
        public string Enlace { get; set; }
        public string RutaImagen { get; set; }
        
        [DefaultValue(false)]
        public bool? Estado { get; set; } = false;
    }
}
