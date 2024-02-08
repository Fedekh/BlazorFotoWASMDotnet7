using BlazorFotoWASMDotnet7.Shared.DB;
using BlazorFotoWASMDotnet7.Shared.Models;
using System.Web.Http.Dependencies;

namespace BlazorFotoWASMDotnet7.Shared.Services
{
    public class FotoRepository : IRepository
    {
        private FotoContext _context;

        public FotoRepository(FotoContext context)
        {
            _context = context;
        }

        public async Task<FotoResponse> GetFotos(string? search = "", int page = 1, bool publicview = false)
        {
            try
            {
                FotoResponse fotoResponse = new FotoResponse();
                IQueryable<Foto> query = _context.Foto.AsQueryable();
                int pageSize = 30;
                int totalItems;

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(f => f.Name.Contains(search));
                    totalItems = query.Count();
                }


                List<Foto>? fotos = query.OrderByDescending(f => f.CreationDate)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                if(publicview == true) fotos = fotos.Where(f => f.IsVisible == true).ToList();//servirà per la chiamata in public

                totalItems = fotos.Count(); 

                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                fotoResponse.Fotos = fotos;
                fotoResponse.CurrentPage = page;
                fotoResponse.TotalResult = totalItems;
                fotoResponse.TotalPages = totalPages;

                return fotoResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle foto.", ex);
            }

        }


        public async Task<Foto> GetFoto(int id)
        {
            return await _context.Foto.FindAsync(id);
        }

        public bool CreateFoto(Foto foto)
        {
            _context.Foto.Add(foto);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateFoto(Foto foto, int id)
        {
            Foto? fotoEdit = _context.Foto.Find(id);
            if (fotoEdit != null)
            {
                fotoEdit.Name = foto.Name;
                fotoEdit.Description = foto.Description;
                fotoEdit.IsVisible = foto.IsVisible;
                fotoEdit.ImageFile = foto.ImageFile;

                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DeleteFoto(int id)
        {
            Foto? fotoDelete = _context.Foto.Find(id);

            if (fotoDelete != null)
            {
                _context.Foto.Remove(fotoDelete);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false; // Foto non trovata
            }
        }

        public async Task<Dictionary<int, bool>> DeleteManyFotos(string ids)
        {
            // tenere traccia dello stato dell'eliminazione di ciascun ID
            Dictionary<int, bool> results = new Dictionary<int, bool>();

            try
            {
                // Punto 1: Validazione dell'input
                if (string.IsNullOrEmpty(ids))
                {
                    Console.WriteLine("Errore: L'input 'ids' non può essere vuoto.");
                    return results;
                }

                List<int> idList;
                try
                {
                    idList = ids?.Split(',').Select(int.Parse).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Errore: Formato non valido per gli ID.");
                    return results;
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Punto 2: Utilizzo delle transazioni
                        List<int> existingIds = _context.Foto
                                                            .Where(f => idList.Contains(f.Id))
                                                            .Select(f => f.Id)
                                                            .ToList();

                        foreach (int id in idList)
                        {
                            if (existingIds.Contains(id))
                            {
                                _context.Foto.RemoveRange(_context.Foto.Where(f => f.Id == id));
                                results[id] = true;
                            }
                            else
                            {
                                results[id] = false;
                            }
                        }

                        await _context.SaveChangesAsync();

                        transaction.Commit(); 
                        return results;
                    }
                    catch (Exception ex)
                    {                     
                        Console.WriteLine($"Errore durante l'eliminazione delle foto: {ex.Message}");
                        transaction.Rollback();
                        return results;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generale durante l'eliminazione delle foto: {ex.Message}");
                return results;
            }
        }


    }
}
