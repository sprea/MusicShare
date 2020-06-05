using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicShare.Data;
using MusicShare.Models;

namespace MusicShare.Pages.Canzoni
{
    [Authorize(Roles = "Admin, User")]
    public class CreateModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        [Obsolete]
        private readonly IHostingEnvironment _environment;

        [Obsolete]
        public CreateModel(MusicShare.Data.ApplicationDbContext context, 
            IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = hostingEnvironment;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["Id_Genere"] = new SelectList(_context.Genere, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Canzone Canzone { get; set; }

        [Display(Name = "Canzone")]
        [Required(ErrorMessage = "Devi caricare il file")]
        [BindProperty]
        public IFormFile MusicFile { get; set; }

        [BindProperty]
        public bool ErrorFile { get; set; }

        [BindProperty]
        public bool ErrorExstensionFile { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        [Obsolete]
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Id_Genere"] = new SelectList(_context.Genere, "Id", "Nome");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Canzone.ApplicationUser = await _userManager.GetUserAsync(User);

            Canzone.Data_Caricamento = DateTime.Now.ToShortDateString();
            

            if (CheckFile(MusicFile))
            {
                ErrorFile = false;

                ErrorExstensionFile = false;

                var UniqueFileName = GetUniqueFileName(MusicFile.FileName);

                Canzone.Nome_file = UniqueFileName;

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                var filePath = Path.Combine(uploadsFolder, UniqueFileName);

                using(var stream = System.IO.File.Create(filePath))
                {
                    await MusicFile.CopyToAsync(stream);    //copio il file nel fs del server
                }
            }else
            {
                return Page();
            }

            _context.Canzone.Add(Canzone);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);

            return Path.GetFileNameWithoutExtension(fileName) + 
                "_" + Guid.NewGuid().ToString().Substring(0, 6) + 
                Path.GetExtension(fileName);
        }

        private string[] AcceptableFormats = new string[]
        {
            ".mp3",
            ".wav",
            ".3gp",
            ".ogg"
        };

        private bool CheckFile(IFormFile file)
        {
            if(file == null)
            {
                return false;
            }

            if (file.Length > 10485760)
            {
                ErrorFile = true;
                return false;
            }

            FileInfo fileInfo = new FileInfo(file.FileName);

            if (!AcceptableFormats.Contains(fileInfo.Extension))
            {
                ErrorExstensionFile = true;
                return false;
            }

            return true;
        }
    }
}
