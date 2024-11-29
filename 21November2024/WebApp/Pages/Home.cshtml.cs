using System.ComponentModel.DataAnnotations;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;

public class Home : PageModel
{

    private readonly IConfigRepository _configRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;

    public Home(IConfigRepository configRepository, IGameRepository gameRepository, IUserRepository userRepository)
    {
       
        _configRepository = configRepository;
        _gameRepository = gameRepository;
        _userRepository = userRepository;
    }

    [BindProperty(SupportsGet = true)]
    public string Username { get; set; } = default!;
    
    
    
    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(Username))
        {
            return RedirectToPage("./Index", new { Error = "No username or password provided!" });
        }
        
        
        return Page();
    }
}