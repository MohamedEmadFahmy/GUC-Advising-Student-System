using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Session;

namespace Milestone_3.Pages
{
    public class LoginModel : PageModel
    {
        public string message = "";
        public bool loginSuccessful;

        private string adminId = "0";
        private string adminPassword = "admin"; 
        public void OnPost()
        {
            // System.Console.WriteLine(Request.Form["Type"]);


            if (Request.Form["Type"].Equals("Student"))
            {
                if (Request.Form["StudentID"] == adminId && Request.Form["password"].Equals(adminPassword))
                {
                    HttpContext.Session.SetString("isAdmin", "true");
                    Response.Redirect("../Admin/Admin");
                }

                StudentLogin();
            }
            else if (Request.Form["Type"].Equals("Advisor"))
            {
                if (Request.Form["AdvisorID"] == adminId && Request.Form["password"].Equals(adminPassword))
                {
                    HttpContext.Session.SetString("isAdmin", "true");
                    Redirect("../Admin/Admin");
                }

                AdvisorLogin();
            }
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


                String sql = "SELECT dbo.FN_StudentLogin(@Student_id, @password)";

                SqlCommand command = new SqlCommand(sql, connection);


                command.Parameters.Add(new SqlParameter("@Student_id", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["StudentID"]) });

                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = Request.Form["password"].ToString() });


                bool success = Convert.ToBoolean(command.ExecuteScalar());

                connection.Close();

                if (success)
                {
                    HttpContext.Session.SetInt32("student_id", Convert.ToInt32(Request.Form["StudentID"]));
                    Response.Redirect("../Student/Student");
                }
                else
                {
                    message = "Incorrect Login Information";
                    //return Page();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:  " + e.ToString());
                // throw;
            }
            //return Page();
        }
        public void AdvisorLogin()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;" +
                    "Initial Catalog=Advising_System;" +
                    "Integrated Security=True;" +
                    "Encrypt=False";


                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();


                String sql = "SELECT dbo.FN_AdvisorLogin(@advisor_id, @password)";

                SqlCommand command = new SqlCommand(sql, connection);


                command.Parameters.Add(new SqlParameter("@advisor_id", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["StudentID"].ToString()) });

                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = Request.Form["password"].ToString() });


                bool success = Convert.ToBoolean(command.ExecuteScalar());


                connection.Close();

                if (success)
                {
                    Response.Redirect("../Advisor/Advisor");
                }
                else
                {
                    message = "Incorrect Login Information or your account is blocked";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:  " + e.ToString());
                //throw;
            }
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
