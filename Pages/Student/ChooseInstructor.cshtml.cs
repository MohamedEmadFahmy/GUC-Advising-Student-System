using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace MyApp.Namespace
{
    public class ChooseInstructorModel : PageModel
    {
        public int studentId = 1;
        public string message = "";
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

                ChooseExam.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = studentId });

                ChooseExam.Parameters.Add(new SqlParameter("@CourseID", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["CourseID"])) });
                ChooseExam.Parameters.Add(new SqlParameter("@instrucorID", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["InstructorID"])) });
                ChooseExam.Parameters.Add(new SqlParameter("@current_semester_code", SqlDbType.NVarChar) { Value = (Request.Form["current_semester_code"]).ToString() });


                ChooseExam.ExecuteNonQuery();

                connection.Close();


            }
            catch (Exception)
            {
                // Console.WriteLine("Exception: " + ex.ToString());
                message = "Error choosing instructor";
                throw;
            }
            message = "Instructor chosen Succesfully";
        }
    }
}
