using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ViewOptionalCoursesModel : PageModel
    {

        public int? studentId;
        public List<Course> Courses = new List<Course>();
        public bool isPosted = false;
        public void OnGet()
        {
            // isPosted = false;
            // Courses = new List<Course>();
            studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                Response.Redirect("../Login/Login");
                return;
            }
        }
        public void OnPost()
        {

            studentId = HttpContext.Session.GetInt32("student_id");

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

                    String sql = "Procedures_ViewOptionalCourse";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    // cmd.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["StudentID"]) });

                    // System.Console.WriteLine($"Student id: {studentId}");
                    // System.Console.WriteLine($"Type: {studentId.GetType}");

                    cmd.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = studentId });
                    // cmd.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = studentId.GetValueOrDefault(0) });
                    // cmd.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = 1 });

                    cmd.Parameters.Add(new SqlParameter("@current_semester_code", SqlDbType.NVarChar) { Value = Request.Form["SemesterCode"].ToString() });

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
    public class Course
    {
        public int courseId;
        public string name;
    }
}
