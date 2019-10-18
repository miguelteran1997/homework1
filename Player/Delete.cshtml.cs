using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OffPractice.Data;

namespace OffPractice.Pages.Player
{
    public class DeleteModel : PageModel
    {
        private readonly OffPractice.Data.ApplicationDbContext _context;

        public DeleteModel(OffPractice.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Players = await _context.Players.FindAsync(id);

            if (Players != null)
            {
                _context.Players.Remove(Players);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
