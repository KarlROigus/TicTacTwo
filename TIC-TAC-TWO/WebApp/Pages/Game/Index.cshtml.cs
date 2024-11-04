using GameBrain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Game;

public class Index : PageModel
{
    public TicTacTwoBrain TicTacTwoBrain { get; set; } = default!;
    
    public void OnGet()
    {
        GameConfiguration defaultConfig = new GameConfiguration();

        var grid = defaultConfig.Grid;

        var gameBoard = defaultConfig.GetFreshGameBoard(defaultConfig, grid);

        GameState initialState = new GameState(grid, 
            gameBoard, 
            defaultConfig, 
            0, 
            EGamePiece.X,
            defaultConfig.PiecesPerPlayer, 
            defaultConfig.PiecesPerPlayer);
        
        TicTacTwoBrain = new TicTacTwoBrain(initialState);
        TicTacTwoBrain.MakeAMove(2, 2);
        TicTacTwoBrain.MakeAMove(2, 3);
    }
}