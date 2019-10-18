using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OffPractice.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffPractice.Pages.Coaches
{
    public class IndexModel : PageModel
    {
        private readonly OffPractice.Data.ApplicationDbContext _context;

        public IndexModel(OffPractice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Coach> Coach { get; set; }

        public async Task OnGetAsync()
        {
            if (User.IsInRole("admin"))
            {
                var Coaches = from c in _context.Coach select c;
                Coaches = Coaches.Where(c => c.CN.Contains("mike"));

                Coach = await _context.Coach.ToListAsync();
            }
        }
    }
}