using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Login : PageModel
{

    private readonly AppDbContext _db;
    
    public Login(AppDbContext dbContext)
    {
        _db = dbContext;
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
        
        var user = _db.Users.FirstOrDefault(each => each.Username == UserName);

        if (user != null)
        {
            return RedirectToPage("./Home", new { UserName = UserName});
        }

        Error = "No username found with the given name! Please try again!";
        return Page();
    }
}