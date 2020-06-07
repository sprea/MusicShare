using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShare.Models
{
    public class Canzone
    {
        [Key]
        public long id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Titolo { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Autore { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Pubblicazione")]
        public string Anno_pubblicazione { get; set; }

        [Display(Name = "Data di caricamento")]
        public string Data_Caricamento { get; set; }

        [Display(Name = "Nome del file")]
        public string Nome_file { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public long Id_Genere { get; set; }

        [ForeignKey("Id_Genere")]
        public Genere Genere { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
