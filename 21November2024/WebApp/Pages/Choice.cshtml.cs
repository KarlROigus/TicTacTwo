using System.Text.Json;
using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;

public class Choice : PageModel
{
    
    private IGameRepository _gameRepository;

    public Choice(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    [BindProperty(SupportsGet = true)] public string UserName { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string GameName { get; set; } = default!;

    [BindProperty]
    public string UserChoice { get; set; } = default!;
    
    public TicTacTwoBrain TicTacTwoBrain { get; set; } = default!;
    
    public SelectList AdvancedOptionSelectList { get; set; } = default!;
    
    public IActionResult OnGet()
    {
        var items = new List<SelectListItem>
        {
            new() { Text = "Add a new piece", Value = "Add" },
            new() { Text = "Move existing piece", Value = "Move" },
            new() { Text = "Change grid position", Value = "Change" }
        };

        AdvancedOptionSelectList = new SelectList(items, "Value", "Text");
        
        var gameJsonString = _gameRepository.GetGameByName(GameName);

        var deSerializedGameState = JsonSerializer.Deserialize<GameState>(gameJsonString)!;

        TicTacTwoBrain = new TicTacTwoBrain(deSerializedGameState);
        
        return Page();
        
    }

    public IActionResult OnPost()
    {

        if (UserChoice == "Add")
        {
            Console.WriteLine("Add");
            return RedirectToPage("./GamePlay", new {UserName, GameName, continuousRequest = true});

        } else if (UserChoice == "Move")
        {
            Console.WriteLine("Move");
            return RedirectToPage("./MovePiece", new {UserName, GameName});
        }
        else
        {
            Console.WriteLine("Grid");
            return RedirectToPage("./ChangeGridPos", new {UserName, GameName});
        }
    }
}