using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class TallerService:ITallerService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadsDirectory;
        public TallerService(ApplicationDbContext context)
        {
            _context = context;
            _uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(_uploadsDirectory))
            {
                Directory.CreateDirectory(_uploadsDirectory);
            }
        }
        public async Task Create(Taller taller, IFormFile file, string uniqueFileName, string ruta, IFormFile fileExpositor, string uniqueExpositorFileName, string expositorFileUrl)
        {
            // Save the first file
            string filePath = Path.Combine(_uploadsDirectory, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            taller.RutaImagen = ruta;

            // Save the second file
            string expositorFilePath = Path.Combine(_uploadsDirectory, uniqueExpositorFileName);
            using (var expositorStream = new FileStream(expositorFilePath, FileMode.Create))
            {
                await fileExpositor.CopyToAsync(expositorStream);
            }
            taller.ExpositorRutaImagen = expositorFileUrl;

            // Save the taller to the database
            _context.Talleres.Add(taller);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Taller taller = await _context.Talleres.FindAsync(id);
            string[] partes = taller.RutaImagen.Split('/');

            // El último elemento en el array partes será el nombre del archivo
            string fileName = partes[partes.Length - 1];

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
            if (File.Exists(filePath))
            {
                // Elimina el archivo
                File.Delete(filePath);
            }
            _context.Talleres.Remove(taller);
            await _context.SaveChangesAsync();
        }
        
        public async Task ToggleStatus(int id)
        {
            Taller ponencia = await _context.Talleres.FindAsync(id);
            if (ponencia != null)
            {
                ponencia.Estado = !ponencia.Estado;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Taller>> GetAll()
        {
            return await _context.Talleres.Where(p => p.Estado == true).ToListAsync();
        }

        public async Task<Taller> GetById(int id)
        {
            return await _context.Talleres.FindAsync(id);
        }

        public async Task Update(Taller taller)
        {
            var tallerOriginal = await _context.Talleres
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TallerId == taller.TallerId);

            taller.RutaImagen = tallerOriginal.RutaImagen;

            _context.Entry(taller).State = EntityState.Modified;

            _context.Entry(taller).Property(t => t.RutaImagen).IsModified = false;

            await _context.SaveChangesAsync();
        }
    }
}
