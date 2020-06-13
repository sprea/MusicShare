using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicShare.Models;

namespace MusicShare.Areas.Identity.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public IList<ApplicationUser> Utenti { get; set; }

        public IActionResult OnGet()
        {
            Utenti = _userManager.GetUsersInRoleAsync("User").Result;

            return Page();
        }
    }
}