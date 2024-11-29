using System.Text.Json;
using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class GamePlay : PageModel
{

    private IGameRepository _gameRepository;

    public GamePlay(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [BindProperty(SupportsGet = true)] public string UserName { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string GameName { get; set; } = default!;

    public string CurrentOneToMove { get; set; } = default!;
    public TicTacTwoBrain TicTacTwoBrain { get; set; } = default!;
    
    public void OnGet()
    {
        var gameJsonString = _gameRepository.GetGameByName(GameName);

        var deSerializedGameState = JsonSerializer.Deserialize<GameState>(gameJsonString)!;

        CurrentOneToMove = deSerializedGameState.CurrentOneToMove;

        TicTacTwoBrain = new TicTacTwoBrain(deSerializedGameState);
    }
    
}