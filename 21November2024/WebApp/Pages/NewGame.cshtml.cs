using ConsoleApp;
using DAL;
using Domain;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;

public class NewGame : PageModel
{
    
    private IGameRepository _gameRepo;
    private IConfigRepository _confRepo;

    public NewGame(IGameRepository gameRepo, IConfigRepository configRepo)
    {
        _gameRepo = gameRepo;
        _confRepo = configRepo;
    }
    
    public SelectList GameTypeSelectList { get; set; } = default!;
    [BindProperty] public string GameType { get; set; } = default!;
    [BindProperty] public string GameName { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string UserName { get; set; } = default!;

    [BindProperty]
    public string ConfigName { get; set; } = default!;
    
    public SelectList ConfigSelectList { get; set; } = default!;
    
    
    public IActionResult OnGet()
    {
        
        var types = new List<SelectListItem>
        {
            new() { Text = "Human VS Human", Value = "HH" },
            new() { Text = "Human VS AI", Value = "HA" },
            new() { Text = "AI VS AI", Value = "AA" }
        };

        GameTypeSelectList = new SelectList(types, "Value", "Text");

        var configNames = _confRepo.GetConfigurationNames(UserName);

        Console.WriteLine(configNames.Count);
        
        var configs = configNames
            .Select(configName => new SelectListItem()
                { Text = configName, Value = configName})
            .ToList();

        ConfigSelectList = new SelectList(configs, "Value", "Text");

        return Page();
    }

    public IActionResult OnPost()
    {
        
        //Get the conf from backend by its name
        
        var gameController = new GameController(UserName, _confRepo, _gameRepo);

        var freshGameState = gameController.GetFreshGameState(new GameConfiguration());

        var ticTacTwoBrain = new TicTacTwoBrain(freshGameState);

        _gameRepo.SaveGame(ticTacTwoBrain.GetGameStateJson(), GameName, UserName);
        

        return RedirectToPage("./GamePlay", new {UserName = UserName, GameName = GameName});
    }
}