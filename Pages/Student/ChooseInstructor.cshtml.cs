using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace MyApp.Namespace
{
    public class ChooseInstructorModel : PageModel
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
                String sql = "Procedures_Chooseinstructor";
                SqlCommand ChooseExam = new SqlCommand(sql, connection);

                ChooseExam.CommandType = CommandType.StoredProcedure;

                ChooseExam.Parameters.Add(new SqlParameter("@studentID", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["studentID"])) });
                ChooseExam.Parameters.Add(new SqlParameter("@courseID", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["courseID"])) });
                ChooseExam.Parameters.Add(new SqlParameter("@InstructorID", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["InstructorID"])) });
                ChooseExam.Parameters.Add(new SqlParameter("@current_semester_code", SqlDbType.NVarChar) { Value = (Request.Form["current_semester_code"]).ToString() });


                ChooseExam.ExecuteNonQuery();

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
