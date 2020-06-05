using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicShare.Data;
using MusicShare.Models;

/*
 * To-Do: Le singole canzoni possono essere eliminate solamente dagli admin 
 * e dal proprietario
 */

namespace MusicShare.Pages.Canzoni
{
    [Authorize(Roles = "Admin, User")]
    public class DeleteModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        [Obsolete]
        private IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public DeleteModel(MusicShare.Data.ApplicationDbContext context, 
            IHostingEnvironment environment)
        {
            _context = context;
            hostingEnvironment = environment;
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
            return Page();
        }

        [Obsolete]
        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Canzone = await _context.Canzone.FindAsync(id);

            if (Canzone != null)
            {
                EliminaFile(Canzone.Nome_file);
                _context.Canzone.Remove(Canzone);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        [Obsolete]
        private void EliminaFile(string Path)
        {
            try
            {
                var uploadsFolder = System.IO.Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var AbsolutePath = System.IO.Path.Combine(uploadsFolder, Canzone.Nome_file);
                System.IO.File.Delete(AbsolutePath);
            }catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
