using System.ComponentModel.DataAnnotations;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MainWebApp.Pages;

public class Home : PageModel
{

    private readonly IConfigRepository _configRepository;

    public Home(IConfigRepository configRepository)
    {
       
        _configRepository = configRepository;
    }

    [BindProperty(SupportsGet = true)]
    public string Username { get; set; } = default!;
    
    [BindProperty(SupportsGet = true)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    public SelectList ConfigSelectList { get; set; } = default!;
    [BindProperty]
    public int ConfigId { get; set; }
    
    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(Username))
        {
            return RedirectToPage("./Index", new { Error = "No username or password provided!" });
        }

        var selectListData = _configRepository.GetConfigurationNames()
            .Select(name => new {id = name, value = name})
            .ToList();

        ConfigSelectList = new SelectList(selectListData, "id",
            "value");
        
        return Page();
    }
}