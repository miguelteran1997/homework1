using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OffPractice.Data;

namespace OffPractice.Pages.Coaches
{
    public class DetailsModel : PageModel
    {
        private readonly OffPractice.Data.ApplicationDbContext _context;

        public DetailsModel(OffPractice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Coach Coach { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coach = await _context.Coach.FirstOrDefaultAsync(m => m.CID == id);

            if (Coach == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
