using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace Milestone_3.Pages
{
    public class LoginModel : PageModel
    {
        public string message = "";
        public bool loginSuccessful;

        private string adminId = "0";
        private string adminPassword = "admin";
        public IActionResult OnPost()
        {
            // System.Console.WriteLine(Request.Form["Type"]);


            if (Request.Form["Type"].Equals("Student"))
            {
                if (Request.Form["StudentID"] == adminId && Request.Form["password"].Equals(adminPassword))
                {
                    return RedirectToPage("../Admin/Admin");
                }

                StudentLogin();
            }
            else if (Request.Form["Type"].Equals("Advisor"))
            {
                if (Request.Form["AdvisorID"] == adminId && Request.Form["password"].Equals(adminPassword))
                {
                    return RedirectToPage("../Admin/Admin");
                }

                return RedirectToPage("../Advisor/Advisor");
            }
            return RedirectToPage();
            // StudentLogin();
        }


        public void StudentLogin()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;" +
                    "Initial Catalog=Advising_System;" +
                    "Integrated Security=True;" +
                    "Encrypt=False";


                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();


                String sql = "FN_StudentLogin";

                SqlCommand command = new SqlCommand(sql, connection);


                command.Parameters.Add(new SqlParameter("@Student_id", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["StudentID"]) });

                command.Parameters.Add(new SqlParameter("@password", SqlDbType.Int) { Value = Request.Form["password"] });


                bool success = (bool)command.ExecuteScalar();

                // RedirectToPage("../Student/Student");

                connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:  " + e.ToString());
                throw;
            }
        }
        public void AdvisorLogin()
        {

        }
    }




    // public class Instructor
    // {
    //     public string id;
    //     public string name;
    //     public string email;
    //     public string faculty;
    //     public string office;
    // }
}
