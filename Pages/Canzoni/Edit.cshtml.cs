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

namespace MusicShare.Pages.Canzoni
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
            
            ViewData["Id_Genere"] = new SelectList(_context.Genere, "Id", "Nome");

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

            _context.Attach(Canzone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanzoneExists(Canzone.id))
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

        private bool CanzoneExists(long id)
        {
            return _context.Canzone.Any(e => e.id == id);
        }
    }
}
