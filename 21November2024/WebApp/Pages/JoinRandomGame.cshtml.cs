using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;

public class JoinRandomGame : PageModel
{

    private IGameRepository _gameRepository;

    public JoinRandomGame(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [BindProperty(SupportsGet = true)] public string UserName { get; set; } = default!;
    public SelectList RandomGameSelectList { get; set; } = default!;

    public string SuccessMessage = default!;

    [BindProperty]
    public string UserChoice { get; set; } = default!;
    
    public IActionResult OnGet()
    {

        var openGameNames = _gameRepository.GetGamesThatCouldBeJoined(UserName);

        var items = openGameNames
            .Select(openGameName => new SelectListItem() 
                { Text = openGameName, Value = openGameName })
            .ToList();


        RandomGameSelectList = new SelectList(items, "Value", "Text");

        return Page();
    }

    public IActionResult OnPost()
    {
        
        _gameRepository.AddSecondPlayer(UserName, UserChoice);

        SuccessMessage = "Game joined successfully! It is now in your LOAD GAMES list";
        
        
        return Page();
    }
}