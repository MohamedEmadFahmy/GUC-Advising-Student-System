using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace MyApp.Namespace
{
    public class ViewAvailableCoursesModel : PageModel
    {
        public List<Course> Courses = new List<Course>();
        public bool isPosted;
        public void OnGet()
        {
            isPosted = false;
            Courses = new List<Course>();

            int? studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                Response.Redirect("../Login/Login");
                return;
            }
        }
        public void OnPost()
        {
            int? studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Assuming your function is named dbo.YourTableViewFunction
                    String sql = "SELECT * FROM dbo.FN_SemsterAvailableCourses(@semstercode)";
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    cmd.CommandType = CommandType.Text;


                    cmd.Parameters.Add(new SqlParameter("@semstercode", SqlDbType.NVarChar) { Value = Request.Form["SemesterCode"].ToString() });

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Course course = new Course
                        {
                            name = reader.GetString(0),
                            courseId = reader.GetInt32(1),
                        };

                        Courses.Add(course);
                    }

                    connection.Close();
                    isPosted = true;
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
