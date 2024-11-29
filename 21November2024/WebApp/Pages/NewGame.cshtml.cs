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
    private AppDbContext _database;

    public NewGame(IGameRepository gameRepo, IConfigRepository configRepo, AppDbContext ctx)
    {
        _gameRepo = gameRepo;
        _database = ctx;
        _confRepo = configRepo;
    }
    
    public SelectList GameTypeSelectList { get; set; } = default!;
    [BindProperty] public string GameType { get; set; } = default!;
    [BindProperty] public string GameName { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string UserName { get; set; } = default!;

    public TicTacTwoBrain? GameBoard { get; set; }
    
    public IActionResult OnGet()
    {
        
        var items = new List<SelectListItem>
        {
            new() { Text = "Human VS Human", Value = "HH" },
            new() { Text = "Human VS AI", Value = "HA" },
            new() { Text = "AI VS AI", Value = "AA" }
        };

        GameTypeSelectList = new SelectList(items, "Value", "Text");

        return Page();
    }

    public IActionResult OnPost()
    {
        
        var gameController = new GameController(UserName, _confRepo, _gameRepo);

        var freshGameState = gameController.GetFreshGameState(new GameConfiguration());

        var ticTacTwoBrain = new TicTacTwoBrain(freshGameState);

        _gameRepo.SaveGame(ticTacTwoBrain.GetGameStateJson(), GameName, UserName);
        

        return RedirectToPage("./GamePlay", new {UserName = UserName, GameName = GameName});
    }
}