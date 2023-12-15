using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace MyApp.Namespace
{
    public class RegisterSecondMakeupModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()
        {
            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();
                String sql = "Procedures_StudentRegisterSecondMakeup";
                SqlCommand Register = new SqlCommand(sql, connection);

                Register.CommandType = CommandType.StoredProcedure;

                Register.Parameters.Add(new SqlParameter("@studentID", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["studentID"])) });
                Register.Parameters.Add(new SqlParameter("@courseID", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["courseID"])) });
                Register.Parameters.Add(new SqlParameter("@Student_Current_Semester", SqlDbType.NVarChar) { Value = (Request.Form["Student_Current_Semester"]).ToString() });


                Register.ExecuteNonQuery();

                connection.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
    }
}
