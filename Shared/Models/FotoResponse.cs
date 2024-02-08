using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFotoWASMDotnet7.Shared.Models
{
    public class FotoResponse
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalResult { get; set; }
        public List<Foto>? Fotos { get; set; }

    }
}
