using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace MyApp.Namespace
{
    public class RegisterFirstMakeupModel : PageModel
    {
        public int? studentId;
        public string message = "";
        public void OnGet()
        {
            studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                Response.Redirect("../Login/Login");
                return;
            }
        }
        public void OnPost()
        {
            if (studentId == null)
            {
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();
                String sql = "Procedures_StudentRegisterFirstMakeup";
                SqlCommand Register = new SqlCommand(sql, connection);

                Register.CommandType = CommandType.StoredProcedure;

                Register.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = studentId });

                Register.Parameters.Add(new SqlParameter("@courseID", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["courseID"]) });
                Register.Parameters.Add(new SqlParameter("@studentCurr_sem", SqlDbType.NVarChar) { Value = Request.Form["Student_Current_Semester"].ToString() });

                Register.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                // throw;
                message = "Error while registering for First Makeup";
            }
            message = "Register for First Makeup Successful";
        }
    }
}
