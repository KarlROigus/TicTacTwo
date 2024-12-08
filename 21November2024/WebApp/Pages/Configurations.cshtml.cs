using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Configurations : PageModel
{

    private IConfigRepository _confRepo;

    public Configurations(IConfigRepository confRepo)
    {
        _confRepo = confRepo;
    }


    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; } = default!;

    public List<GameConfiguration> Configs { get; set; } = default!;
    
    public void OnGet()
    {

        Configs = _confRepo.GetUserConfigurations(UserName);
        
        
    }
}