using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicShare.Data;
using MusicShare.Models;

namespace MusicShare.Pages.Canzoni
{
    [Authorize(Roles = "Admin, User")]
    public class CreateModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public CreateModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Id_Genere"] = new SelectList(_context.Genere, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Canzone Canzone { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Canzone.Add(Canzone);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
