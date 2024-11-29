using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Login : PageModel
{
    
    private readonly IUserRepository _userRepository;
    
    public Login( IUserRepository userRepository)
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
            Error = "Cannot log in with empty username!";
            return Page();
        }
        
        var userName = _userRepository.FindUserByName(UserName);

        if (userName != null)
        {
            return RedirectToPage("./Home", new { UserName = UserName});
        }

        Error = "No username found with the given name! Please try again!";
        return Page();
    }
}