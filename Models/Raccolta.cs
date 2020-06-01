using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShare.Models
{
    public class Raccolta
    {
        [Key]
        public long Id { get; set; }

        public string Nome { get; set; }
    }
}
