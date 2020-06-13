using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicShare.Models;

namespace MusicShare.Areas.Identity.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser Utente { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            if (id == null)
                return NotFound();

            Utente = _userManager.FindByIdAsync(id).Result;

            if (Utente == null)
                return NotFound();

            var chech = await _userManager.DeleteAsync(Utente);

            if (chech.Succeeded)
                return RedirectToPage("./Index");

            return Page();
        }
    }
}