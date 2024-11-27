using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)] public string? Error { get; set; }
    [BindProperty] public string? Username { get; set; }
    [BindProperty] [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")] public string? Password { get; set; }
    
    
    public IActionResult OnPost()
    {
        Username = Username?.Trim();
        
        if (!ModelState.IsValid)
        {
            // If the model state is invalid, redisplay the form with validation messages
            return Page();
        }

        if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
        {
            Username = ComputeSha256Hash(Username);
            
            return RedirectToPage("./Home", new {Username, Password});
        }
        else
        {
            Error = "Please insert some username and password!";
            return Page();
        }
        
    }
    
    static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256 instance
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Compute the hash as a byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a hexadecimal string
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2")); // x2 for lowercase hexadecimal
            }
            return builder.ToString();
        }
    }
}