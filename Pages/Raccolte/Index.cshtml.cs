using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicShare.Data;
using MusicShare.Models;

namespace MusicShare.Pages.Raccolte
{
    [Authorize(Roles = "Admin, User")]
    public class IndexModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Raccolta> Raccolta { get;set; }

        public ApplicationUser user { get; set; }

        public async Task OnGetAsync()
        {
            user = await _userManager.GetUserAsync(User);

            Raccolta = await _context.Raccolta.Include(c=>c.RaccoltaCanzoni)
                .Where(c => c.ApplicationUser == user)
                .ToListAsync();
        }
    }
}
