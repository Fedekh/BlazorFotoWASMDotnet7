using BlazorFotoWASMDotnet7.Shared.Models;

namespace BlazorFotoWASMDotnet7.Shared.Services
{
    public interface IRepository
    {
        public Task<FotoResponse> GetFotos(string? search, int page = 1, bool publicview = false);
        public Task<Foto> GetFoto(int id);
        public bool CreateFoto(Foto foto);
        public bool UpdateFoto(Foto foto, int id);
        public bool DeleteFoto(int id);
        public Task<Dictionary<int, bool>> DeleteManyFotos(string ids);
    }
}