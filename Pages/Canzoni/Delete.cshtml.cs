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
    [Authorize(Roles = "Admin, User")]
    public class DeleteModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public DeleteModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Canzone Canzone { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Canzone = await _context.Canzone
                .Include(c => c.Genere).FirstOrDefaultAsync(m => m.id == id);

            if (Canzone == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Canzone = await _context.Canzone.FindAsync(id);

            if (Canzone != null)
            {
                _context.Canzone.Remove(Canzone);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
