using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    
    public SelectList LoginSelectList { get; set; } = default!;

    [BindProperty] public string UserChoice { get; set; } = default!;
    
    public IActionResult OnGet()
    {
        
        var items = new List<SelectListItem>
        {
            new() { Text = "Create new user", Value = "Create" },
            new() { Text = "Login with existing user", Value = "Login" }
        };

        LoginSelectList = new SelectList(items, "Value", "Text");

        return Page();
    }
    
    public IActionResult OnPost()
    {
        return RedirectToPage(UserChoice == "Create" ? "./Create" : "./Login");
    }
    
}