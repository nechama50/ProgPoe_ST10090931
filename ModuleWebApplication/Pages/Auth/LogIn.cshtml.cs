using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModuleModelLibrary;
using POE.EntityFramework.Models;

namespace ModuleWebApplication.Pages.Auth
{
    public class LogInModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public IAuthHandler AuthHandler { get; }

        //creating constructor - using lambda - taking in dependancy injection
        public LogInModel(IAuthHandler authhandler) => AuthHandler = authhandler; 


        //
        public async Task<IActionResult> OnPostAsync()
        {
              //1. go to AuthHandler
             //2. AuthHandler will call AuthService (database layer)
            //3. Compare/capture the entered values to the values in the database

            try
            {
                //set the return value for the backgroundworker
                User? Result = await AuthHandler.LogInUser(UserName, Password);

                //calling authentication handeler to log in user
                if (Result != null)
                {
                    //setting the user name after user is logged in
                    HttpContext.Session.SetInt32("UserId" , Result.Id);

                    //calling the next window 
                    return RedirectToPage("/Modules/ModuleInformation");

                }
                else
                {
                    // MessageBox.Show("There was an error please try again or register as a new user or check the entered credentials");
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
