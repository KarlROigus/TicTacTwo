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

    public bool GameIsOver { get; set; } = false;
    
    public IActionResult OnGet(int? x, int? y, bool continuousRequest = false)
    {
        
        
        var gameJsonString = _gameRepository.GetGameByName(GameName);

        var deSerializedGameState = JsonSerializer.Deserialize<GameState>(gameJsonString)!;

        TicTacTwoBrain = new TicTacTwoBrain(deSerializedGameState);
        
        CurrentOneToMove = TicTacTwoBrain.GetCurrentOneToMove();
        
        if (TicTacTwoBrain.GetMovesMade() >=
            deSerializedGameState.GameConfiguration.HowManyMovesTillAdvancedGameMoves * 2
            && !continuousRequest && CurrentOneToMove == UserName 
            && deSerializedGameState.GameConfiguration.HowManyMovesTillAdvancedGameMoves != ConstantlyUsed.ClassicalGame)
        {
            return RedirectToPage("./Choice", new { UserName, GameName });
        }
        

        CurrentOneToMove = TicTacTwoBrain.GetCurrentOneToMove();

        if (CurrentOneToMove == "" && deSerializedGameState.LoginUser != UserName)
        {
            CurrentOneToMove = UserName;
        }
        
        if (x != null && y != null)
        {
            
            var moveWasSuccessful = TicTacTwoBrain.MakeAMove(x.Value, y.Value);
            if (moveWasSuccessful)
            {

                if (TicTacTwoBrain.SomebodyHasWon())
                {
                    GameIsOver = true;
                    return Page();
                }
                
                TicTacTwoBrain.ReducePieceCountForPlayer();
                var playerXName = _gameRepository.GetPlayerName(GameName, "X");
                var playerOName = _gameRepository.GetPlayerName(GameName, "O");
                
                TicTacTwoBrain.ToggleCurrentOneToMove(playerXName!, playerOName);

                CurrentOneToMove = TicTacTwoBrain.GetCurrentOneToMove();
                _gameRepository.SaveGame(TicTacTwoBrain.GetGameStateJson(), 
                    GameName, UserName);
            }
            
        }

        return Page();
    }
    
}