using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModuleModelLibrary;

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

        public void OnGet()
        {

        }

        //
        public async Task<IActionResult> OnPostAsync()
        {
              //1. go to AuthHandler
             //2. AuthHandler will call AuthService (database layer)
            //3. Compare the entered values to the values in the database

            try
            {
                //set the return value for the backgroundworker
                bool Result = await AuthHandler.LogInUser(UserName, Password);

                //calling authentication handeler to log in user
                if (Result == true)
                {
                    //setting the user name after user is logged in
                    HttpContext.Session.SetString("UserName" , UserName);

                    //calling the next window 
                    return RedirectToPage("/Index");

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
