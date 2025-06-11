using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly ApplicationDbContext _context;

        public PhotoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Photo>> GetAllPhotosAsync()
        {
            return await _context.Photos.ToListAsync();
        }

        public async Task<Photo> GetPhotoByIdAsync(int id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public async Task CreatePhotoAsync(Photo photo)
        {
            _context.Add(photo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhotoAsync(Photo photo)
        {
            _context.Update(photo);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> PhotoExistsAsync(int id)
        {
            return await _context.Photos.AnyAsync(e => e.Id == id);
        }
    }
} 