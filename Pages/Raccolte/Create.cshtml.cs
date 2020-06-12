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
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Raccolta Raccolta { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Raccolta.ApplicationUser = await _userManager.GetUserAsync(User);

            Raccolta.Data_Creazione = DateTime.Now.ToShortDateString();

            _context.Raccolta.Add(Raccolta);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
