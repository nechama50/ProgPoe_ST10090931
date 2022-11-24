using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModuleModelLibrary;
using POE.EntityFramework.Models;

namespace ModuleWebApplication.Pages.Modules
{
    public class DisplayModel : PageModel
    {
        [BindProperty]
        public List<Module> Modules { get; set; }
        private  IModuleHandler ModuleHandler { get; set; }

        //creating constructor - using lambda - taking in dependancy injection
        public DisplayModel(IModuleHandler modulehandler) 
        {
            ModuleHandler = modulehandler;
            Modules = new List<Module>();   
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //getting user id
            int? userId = HttpContext.Session.GetInt32("UserId");

            //checking if user is logged in
            if(userId != null)
            {
                //ternery expression
                Modules = userId != null? await ModuleHandler.GetAllModulesAsync((int)userId) : 
                    new List<Module>();
                

                return Page();
            } else
            {
                
                return RedirectToPage("/Auth/LogIn");
            }
        }

        public async Task<IActionResult> OnPostRemove(int Id)
        {
            //getting user id
            int? userId = HttpContext.Session.GetInt32("UserId");

            //calling module handler to delete the module 
            bool deleteResult = await ModuleHandler.DeleteModuleAsync((int)userId , Id);

            if (deleteResult)
            {
                return RedirectToPage("/Modules/Display");
            } else
            {
                return Page();
            }
                
        }
    }
}
