using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffPractice.Data;

namespace OffPractice.Pages.Player
{
    public class EditModel : PageModel
    {
        private readonly OffPractice.Data.ApplicationDbContext _context;

        public EditModel(OffPractice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Players Players { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Players = await _context.Players
                .Include(p => p.Coaches)
                .Include(p => p.Teams).FirstOrDefaultAsync(m => m.PID == id);

            if (Players == null)
            {
                return NotFound();
            }
           ViewData["CID"] = new SelectList(_context.Coach, "CID", "CID");
           ViewData["TID"] = new SelectList(_context.Team, "TID", "TID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Players).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayersExists(Players.PID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlayersExists(int id)
        {
            return _context.Players.Any(e => e.PID == id);
        }
    }
}
