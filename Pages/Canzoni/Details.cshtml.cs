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

namespace MusicShare.Pages.Canzoni
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public DetailsModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Canzone Canzone { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Canzone = await _context.Canzone.Include(c => c.Genere).Include(c => c.ApplicationUser).FirstOrDefaultAsync(m => m.id == id);

            if (Canzone == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
