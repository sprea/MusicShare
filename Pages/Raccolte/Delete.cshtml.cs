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
    public class DeleteModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public DeleteModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Raccolta Raccolta { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Raccolta = await _context.Raccolta.FirstOrDefaultAsync(m => m.Id == id);

            if (Raccolta == null)
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

            Raccolta = await _context.Raccolta.FindAsync(id);

            if (Raccolta != null)
            {
                _context.Raccolta.Remove(Raccolta);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
