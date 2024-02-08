using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFotoWASMDotnet7.Shared.Models
{
    public class Foto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "L'immagine deve per forza avere un titolo")]
        [MinLength(5, ErrorMessage = "Il titolo deve contenere almeno 5 caratteri.")]
        [MaxLength(30, ErrorMessage = "Il titolo non può avere più di 30 caratteri.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "L'immagine deve avere una descrizione")]
        public string Description { get; set; }

        public bool IsVisible { get; set; } = true;

        public byte[]? ImageFile { get; set; }

        [NotMapped]
        public string ImagePath => ImageFile is null ? "https://cdn.dribbble.com/users/476251/screenshots/2619255/attachments/523315/placeholder.png?resize=400x300&vertical=center"
                                                    : $"data:image/jpeg;base64, {Convert.ToBase64String(ImageFile)}";

        public DateTime CreationDate { get; set; } = DateTime.Now; // Proprietà per la data di creazione

        public Foto() { }

        public Foto(string name, string description, bool isVisible, byte[]? imageFile)
        {
            Name = name;
            Description = description;
            IsVisible = isVisible;
            ImageFile = imageFile;
            CreationDate = DateTime.Now;
        }
    }
}
