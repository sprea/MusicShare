using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly MusicShare.Data.ApplicationDbContext _context;

        public IndexModel(MusicShare.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Canzone> Canzone { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Titolo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Autore { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Anno di pubblicazione")]
        public string AnnoPubblicazione { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Genere { get; set; }

        public async Task OnGetAsync()
        {
            ViewData["Genere"] = new SelectList(_context.Genere, "Id", "Nome");

            var canzoni = from m in _context.Canzone select m;

            if(!string.IsNullOrEmpty(Titolo))
            {
                canzoni = canzoni.Where(s => s.Titolo.Contains(Titolo));
            }

            if (!string.IsNullOrEmpty(Autore))
            {
                canzoni = canzoni.Where(s => s.Autore.Contains(Autore));
            }

            if (!string.IsNullOrEmpty(AnnoPubblicazione))
            {
                canzoni = canzoni.Where(s => s.Anno_pubblicazione == AnnoPubblicazione);
            }

            if (!string.IsNullOrEmpty(Genere))
            {
                canzoni = canzoni.Where(s => s.Genere.Id.ToString() == Genere);
            }

            Canzone = await canzoni.Include(x => x.Genere).Include(x => x.ApplicationUser).ToListAsync();

            //Canzone = await _context.Canzone.Include(c => c.Genere).Include(c=>c.ApplicationUser).ToListAsync();
        }
    }
}
