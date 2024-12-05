using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;

public class LoadGame : PageModel
{

    private IGameRepository _gameRepository;
    
    
    public LoadGame(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    [BindProperty(SupportsGet = true)] public string UserName { get; set; } = default!;
    public SelectList LoadGameSelectList { get; set; } = default!;
    
    public string SuccessMessage = default!;
    
    [BindProperty]
    public string UserChoice { get; set; } = default!;
    
    public IActionResult OnGet()
    {

        var mySavedGameNames = _gameRepository.GetGamesImPartOf(UserName);
        
        var items = mySavedGameNames
            .Select(openGameName => new SelectListItem() 
                { Text = openGameName, Value = openGameName })
            .ToList();


        LoadGameSelectList = new SelectList(items, "Value", "Text");

        return Page();
    }

    public IActionResult OnPost()
    {

        return RedirectToPage("./GamePlay", new { UserName = UserName, GameName = UserChoice });
    }
}