using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Create : PageModel
{

    private readonly AppDbContext _db;
    
    public Create(AppDbContext dbContext)
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
            Error = "Cannot make an account with empty username!";
            return Page();
        }
        
        var user = _db.Users.FirstOrDefault(each => each.Username == UserName);

        if (user == null)
        {
            Console.WriteLine("HERE");
            _db.Users.Add(new User()
            {
                Username = UserName
            });
            _db.SaveChanges();
            return RedirectToPage("./Home", new { UserName = UserName });
        }

        Error = "The username already exists! Please try again!";
        return Page();
    }
}