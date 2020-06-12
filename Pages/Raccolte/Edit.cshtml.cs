using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShare.Data;
using MusicShare.Models;

namespace MusicShare.Pages.Raccolte
{
    [Authorize(Roles = "Admin, User")]
    public class EditModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public EditModel(MusicShare.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Raccolta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaccoltaExists(Raccolta.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RaccoltaExists(long id)
        {
            return _context.Raccolta.Any(e => e.Id == id);
        }
    }
}
