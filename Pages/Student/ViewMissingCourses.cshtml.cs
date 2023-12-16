using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ViewMissingCoursesModel : PageModel
    {
        public int? studentId;
        public List<Course> Courses = new List<Course>();
        public void OnGet()
        {

            studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                Response.Redirect("../Login/Login");
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "Procedures_ViewMS";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = studentId });

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Course course = new Course
                        {
                            courseId = reader.GetInt32(0),
                            name = reader.GetString(1),
                        };


                        Courses.Add(course);
                    }

                    connection.Close();
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
