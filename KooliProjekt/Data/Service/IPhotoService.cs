using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> GetAllPhotosAsync();
        Task<Photo> GetPhotoByIdAsync(int id);
        Task<Photo> CreatePhotoAsync(Photo photo);
        Task<Photo> UpdatePhotoAsync(Photo photo);
        Task DeletePhotoAsync(int id);
    }
}
