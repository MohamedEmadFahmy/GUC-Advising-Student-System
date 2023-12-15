using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminLinkStudentToCourseModel : PageModel
    {
        public string Message { get; set; } = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Procedures_AdminLinkStudent";
                    SqlCommand linkStudentToCourse = new SqlCommand(sql, connection);
                    linkStudentToCourse.CommandType = CommandType.StoredProcedure;

                    int? courseId = !string.IsNullOrEmpty(Request.Form["cours_id"]) ? Convert.ToInt32(Request.Form["cours_id"]) : (int?)null;
                    int? instructorId = !string.IsNullOrEmpty(Request.Form["instructor_id"]) ? Convert.ToInt32(Request.Form["instructor_id"]) : (int?)null;
                    int? studentId = !string.IsNullOrEmpty(Request.Form["studentID"]) ? Convert.ToInt32(Request.Form["studentID"]) : (int?)null;
                    string semesterCode = Request.Form["semester_code"];

                    if (courseId == null || instructorId == null || studentId == null || string.IsNullOrEmpty(semesterCode))
                    {
                        Console.WriteLine("One of the inputs is null");
                        return;
                    }

                    linkStudentToCourse.Parameters.Add(new SqlParameter("@cours_id", SqlDbType.Int) { Value = courseId });
                    linkStudentToCourse.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int) { Value = instructorId });
                    linkStudentToCourse.Parameters.Add(new SqlParameter("@studentID", SqlDbType.Int) { Value = studentId });
                    linkStudentToCourse.Parameters.Add(new SqlParameter("@semester_code", SqlDbType.VarChar) { Value = semesterCode });

                    linkStudentToCourse.ExecuteNonQuery();
                    Message = "Student linked to the course successfully!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
    }
}
