using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Milestone_3.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            // return RedirectToPage("./Register/StudentRegister");
            // return RedirectToPage("./Student/Student");
            return RedirectToPage("./Login/Login");
        }
    }
}
