using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
            var result = HttpContext.Session.GetInt32("isAdmin");

            if (result == null)
            {
                Response.Redirect("../Login/Login");
            }
        }
    }
}
