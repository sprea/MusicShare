using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShare.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }

        public string Cognome { get; set; }

        public Raccolta Raccolta { get; set; }
    }
}
