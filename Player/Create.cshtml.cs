using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OffPractice.Data;

namespace OffPractice.Pages.Player
{
    public class CreateModel : PageModel
    {
        private readonly OffPractice.Data.ApplicationDbContext _context;

        public CreateModel(OffPractice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CID"] = new SelectList(_context.Coach, "CID", "CID");
        ViewData["TID"] = new SelectList(_context.Team, "TID", "TID");
            return Page();
        }

        [BindProperty]
        public Players Players { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Players.Add(Players);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}