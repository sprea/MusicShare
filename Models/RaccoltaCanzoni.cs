using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShare.Models
{
    public class RaccoltaCanzoni
    {
        public long Id_Raccolta { get; set; }

        public long Id_Canzone { get; set; }

        public Raccolta Raccolta { get; set; }

        public Canzone Canzone { get; set; }
    }
}
