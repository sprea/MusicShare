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

namespace MusicShare.Pages.Generi
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public CreateModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Genere Genere { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Genere.Add(Genere);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
