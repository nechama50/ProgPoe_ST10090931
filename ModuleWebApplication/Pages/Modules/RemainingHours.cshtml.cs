using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModuleModelLibrary;
using POE.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;

namespace ModuleWebApplication.Pages.Modules
{
    public class RemainingHoursModel : PageModel
    {
        [BindProperty]
        public int HoursWorked { get; set; }
        [BindProperty]
        public DateTime TrackedDate { get; set; }
        [Display(Name = "Modules List")]
        public SelectList? ModulesSL { get; set; }
        [BindProperty]
        public int SelectedModule { get; set; }
        [BindProperty]
        public bool Submited { get; set; }
        [BindProperty]
        public Module UpdatedModule { get; set; }

        public IModuleHandler ModuleHandler { get; set; }

        //creating constructor - using lambda - taking in dependancy injection
        public RemainingHoursModel(IModuleHandler modulehandler) 
        {
            ModuleHandler = modulehandler;
            Submited = false;
        } 

        public async Task<IActionResult> OnGetAsync()
        {
       
            //getting user id
            int? userId = HttpContext.Session.GetInt32("UserId");

            //checking if user is logged in
            if (userId != null)
            {
                IEnumerable<Module> modules = await ModuleHandler.GetModulesEnumerablesAsync((int)userId);
                ModulesSL = new SelectList(modules , "Id" , "Code");

                return Page();
            }
            else
            {

                return RedirectToPage("/Auth/LogIn");
            }
        }

        public async Task OnPostAsync()
        {
            //get user id from sessions 
            int? userId = HttpContext.Session.GetInt32("UserId");

            //updates the selected module 
            Module? updatedModule = await ModuleHandler.UpdateRemainingHoursAsync((int)userId,SelectedModule,HoursWorked);

            if(updatedModule != null)
            {
               UpdatedModule = updatedModule;
                Submited = true;
            }

        }
    }
}
