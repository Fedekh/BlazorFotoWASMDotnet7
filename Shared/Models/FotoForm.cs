using Microsoft.AspNetCore.Http;


namespace BlazorFotoWASMDotnet7.Shared.Models
{
    public class FotoForm
    {
        public Foto? Foto { get; set; }
        public IFormFile? Image {  get; set; }
    }
}
