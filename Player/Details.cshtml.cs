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
    public class DetailsModel : PageModel
    {
        private readonly OffPractice.Data.ApplicationDbContext _context;

        public DetailsModel(OffPractice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
