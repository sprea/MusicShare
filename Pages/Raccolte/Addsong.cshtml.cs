using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicShare.Data;
using MusicShare.Models;

namespace MusicShare.Pages.Raccolte
{
    [Authorize(Roles = "Admin, User")]
    public class AddsongModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddsongModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ApplicationUser utente { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            utente = await _userManager.GetUserAsync(User);

            ViewData["Id_Canzone"] = new SelectList(_context.Canzone, "id", "Titolo");
            ViewData["Id_Raccolta"] = new SelectList(_context.Raccolta.Where(c =>c.ApplicationUser == utente), "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public RaccoltaCanzoni RaccoltaCanzoni { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Id_Canzone"] = new SelectList(_context.Canzone, "id", "Titolo");
            ViewData["Id_Raccolta"] = new SelectList(_context.Raccolta.Where(c => c.ApplicationUser == utente), "Id", "Nome");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RaccoltaCanzoni.Add(RaccoltaCanzoni);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
