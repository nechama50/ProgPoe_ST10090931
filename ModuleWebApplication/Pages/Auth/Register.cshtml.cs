using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModuleModelLibrary;
using NuGet.Protocol.Plugins;
using POE.EntityFramework.Models;

namespace ModuleWebApplication.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public IAuthHandler AuthHandler { get; }
        private static readonly IPasswordHasher<User> PasswordHasher = new PasswordHasher<User>();

        //creating constructor - using lambda - taking in dependancy injection
        public RegisterModel(IAuthHandler authhandler) => AuthHandler = authhandler;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                User.PasswordHash = PasswordHasher.HashPassword(User , User.PasswordHash);

                try
                {
                    //set the return value for the backgroundworker
                    bool Result = await AuthHandler.CreateUser(User);

                    //creating user
                    if (Result == true)
                    {
                        //redirect to log in page 
                        return RedirectToPage("/Auth/LogIn");
                    }
                    else
                    {
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    return Page();
                }

            }
            catch (Exception ex)
            {
                return Page();
            }

         }
    }
}
