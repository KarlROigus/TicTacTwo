using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace MainWebApp.Pages.GameStates
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<GameState> GameState { get;set; } = default!;

        public async Task OnGetAsync()
        {
            GameState = await _context.GameStates
                .Include(g => g.Config).ToListAsync();
        }
    }
}
