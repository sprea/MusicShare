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

namespace MusicShare.Pages.Raccolte
{
    [Authorize(Roles = "Admin, User")]
    public class DetailsModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public DetailsModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Raccolta Raccolta { get; set; }

        [BindProperty]
        public List<RaccoltaCanzoni> raccoltaCanzoni { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Raccolta = await _context.Raccolta.FirstOrDefaultAsync(m => m.Id == id);

            raccoltaCanzoni = await _context.RaccoltaCanzoni.Include(c=>c.Canzone).Include(c=>c.Raccolta)
                .Where(c=>c.Id_Raccolta == id)
                .ToListAsync();

            if (Raccolta == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
