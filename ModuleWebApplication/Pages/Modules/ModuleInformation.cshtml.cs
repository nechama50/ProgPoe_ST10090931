using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModuleModelLibrary;
using POE.EntityFramework.Models;

namespace ModuleWebApplication.Pages.Modules
{
    public class ModuleInformationModel : PageModel
    {
        [BindProperty]
        public Module Module { get; set; }
        public IModuleHandler ModuleHandler { get; set; }


        //creating constructor - using lambda - taking in dependancy injection
        public ModuleInformationModel(IModuleHandler modulehandler) => ModuleHandler = modulehandler;

        public async Task<IActionResult> OnGetAsync()
        {
            //getting user id
            int? userId = HttpContext.Session.GetInt32("UserId");

            //checking if user is logged in
            if (userId != null)
            {
              
                return Page();
            }
            else
            {

                return RedirectToPage("/Auth/LogIn");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            //Go to ModuleHandler
            bool Result = await ModuleHandler.CreateModule((int)userId, Module);

            if(Result)
            {
                return RedirectToPage("/Modules/Display");
            } else
            {
                return Page();
            }

        }
    }
}
