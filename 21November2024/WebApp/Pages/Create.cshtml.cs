using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Create : PageModel
{

    private readonly IUserRepository _userRepository;
    
    public Create(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    
    [BindProperty]
    public string UserName { get; set; } = default!;
    
    public string Error { get; set; } = default!;

    

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(UserName))
        {
            Error = "Cannot make an account with empty username!";
            return Page();
        }

        var user = _userRepository.FindUserByName(UserName);

        if (user == null)
        {
            _userRepository.CreateNewUser(UserName);
            
            
            return RedirectToPage("./Home", new { UserName = UserName });
        }

        Error = "The username already exists! Please try again!";
        return Page();
    }
}