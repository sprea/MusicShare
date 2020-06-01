using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicShare.Data;
using MusicShare.Models;

namespace MusicShare.Pages.Generi
{
    [Authorize(Roles = "Admin, User")]
    public class IndexModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public IndexModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Genere> Genere { get;set; }

        public async Task OnGetAsync()
        {
            Genere = await _context.Genere.ToListAsync();
        }
    }
}
