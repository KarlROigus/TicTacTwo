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
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public GameState GameState { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamestate = await _context.GameStates.FirstOrDefaultAsync(m => m.Id == id);
            if (gamestate == null)
            {
                return NotFound();
            }
            else
            {
                GameState = gamestate;
            }
            return Page();
        }
    }
}
