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
    public class DetailsModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public DetailsModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Genere Genere { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genere = await _context.Genere.FirstOrDefaultAsync(m => m.Id == id);

            if (Genere == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
