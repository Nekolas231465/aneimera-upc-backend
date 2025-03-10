using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class PonenciaService : IPonenciaService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadsDirectory;
        public PonenciaService(ApplicationDbContext context)
        {
            _context = context;
            _uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(_uploadsDirectory))
            {
                Directory.CreateDirectory(_uploadsDirectory);
            }
        }

        public async Task Create(Ponencia ponencia, IFormFile file, string uniqueFileName, string ruta)
        {
            string filePath = Path.Combine(_uploadsDirectory, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ponencia.RutaImagen = ruta;
            _context.Ponencias.Add(ponencia);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Ponencia ponencia = await _context.Ponencias.FindAsync(id);
            string[] partes = ponencia.RutaImagen.Split('/');

            string fileName = partes[partes.Length - 1];

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _context.Ponencias.Remove(ponencia);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ponencia>> GetAll()
        {
            return await _context.Ponencias.ToListAsync();
        }

        public async Task<Ponencia> GetById(int id)
        {
            return await _context.Ponencias.FindAsync(id);
        }

        public async Task<Ponencia> GetRecent()
        {
            return await _context.Ponencias.OrderByDescending(p => p.Fecha)
                         .FirstOrDefaultAsync();
        }

        public async Task Update(Ponencia ponencia)
        {
            var ponenciaOriginal = await _context.Ponencias
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.PonenciaId == ponencia.PonenciaId);

            ponencia.RutaImagen = ponenciaOriginal.RutaImagen;

            _context.Entry(ponencia).State = EntityState.Modified;

            _context.Entry(ponencia).Property(p => p.RutaImagen).IsModified = false;

            await _context.SaveChangesAsync();
        }
    }
}
