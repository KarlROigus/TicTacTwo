using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Options : PageModel
{


    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; } = default!;
    
    public void OnGet()
    {
        
    }
}