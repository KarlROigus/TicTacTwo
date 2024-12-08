using System.ComponentModel.DataAnnotations;
using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class ConfigCreation : PageModel
{

    private IConfigRepository _confRepo;

    public ConfigCreation(IConfigRepository confRepo)
    {
        _confRepo = confRepo;
    }

    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; } = default!;

    [BindProperty]
    public string ConfigName { get; set; } = default!;

    [BindProperty]
    [Range(5, 49)]
    public int BoardWidth { get; set; }

    [BindProperty]
    [Range(5, 49)]
    public int GridWidth { get; set; }

    public string ErrorMessage { get; set; } = default!;

    public string SuccessMessage { get; set; } = default!;

    

    [BindProperty]
    public int HowManyMovesTillAdvancedMoves { get; set; }
    
    [BindProperty]
    public int PiecesPerPlayer { get; set; }


    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        if (GridWidth > BoardWidth)
        {
            ErrorMessage = "Grid width cannot be greater than board width!";
            return Page();
        }

        if (PiecesPerPlayer < GridWidth)
        {
            ErrorMessage = "Player can not have less pieces than win condition!";
            return Page();
        }
        

        var newConf = new GameConfiguration()
        {
            Name = ConfigName,
            HowManyMovesTillAdvancedGameMoves = HowManyMovesTillAdvancedMoves,
            PiecesPerPlayer = PiecesPerPlayer,
            WinCondition = GridWidth,
            BoardDimension = BoardWidth,
            GridDimension = GridWidth,
            Grid = new Grid(BoardWidth / 2, BoardWidth / 2, BoardWidth, GridWidth)
        };
        
        _confRepo.AddNewConfiguration(newConf, UserName);

        SuccessMessage = "New configuration added successfully!";
        
        return Page();
    }
}