using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class VisitaTecnicaService:IVisitaTecnicaService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadsDirectory;
        public VisitaTecnicaService(ApplicationDbContext context)
        {
            _context = context;
            _uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(_uploadsDirectory))
            {
                Directory.CreateDirectory(_uploadsDirectory);
            }
        }
        public async Task Create(VisitaTecnica visitaTecnica, IFormFile file, string uniqueFileName, string ruta)
        {
            string filePath = Path.Combine(_uploadsDirectory, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            visitaTecnica.RutaImagen = ruta;
            _context.VisitaTecnicas.Add(visitaTecnica);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            VisitaTecnica visitaTecnica  = await _context.VisitaTecnicas.FindAsync(id);
            string[] partes = visitaTecnica.RutaImagen.Split('/');

            string fileName = partes[partes.Length - 1];

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _context.VisitaTecnicas.Remove(visitaTecnica);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VisitaTecnica>> GetAll()
        {
            return await _context.VisitaTecnicas.ToListAsync();
        }

        public async Task<VisitaTecnica> GetById(int id)
        {
            return await _context.VisitaTecnicas.FindAsync(id);
        }

        public async Task Update(VisitaTecnica visitaTecnica)
        {
            var visitaTecnicaOriginal = await _context.VisitaTecnicas
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.VisitaTecninaId == visitaTecnica.VisitaTecninaId);

            visitaTecnica.RutaImagen = visitaTecnicaOriginal.RutaImagen;

            _context.Entry(visitaTecnica).State = EntityState.Modified;

            _context.Entry(visitaTecnica).Property(p => p.RutaImagen).IsModified = false;

            await _context.SaveChangesAsync();
        }
    }
}
