﻿using Microsoft.AspNetCore.Http;
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

        public string Titolo { get; set; }

        public string Autore { get; set; }

        [Display(Name = "Data di caricamento")]
        public DateTime Data_Caricamento = DateTime.UtcNow;

        public string Nome_file { get; set; }

        public long Id_Genere { get; set; }

        [ForeignKey("Id_Genere")]
        public Genere Genere { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
