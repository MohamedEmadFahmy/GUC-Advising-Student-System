using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ViewOptionalCoursesModel : PageModel
    {
        public List<Course> Courses = new List<Course>();
        public bool isPosted = false;
        public void OnGet()
        {
            isPosted = false;
        }
        public void OnPost()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "Procedures_ViewOptionalCourse";
                    SqlCommand viewOptionalCourse = new SqlCommand(sql, connection);
                    viewOptionalCourse.CommandType = CommandType.StoredProcedure;


                    viewOptionalCourse.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["StudentID"]) });

                    viewOptionalCourse.Parameters.Add(new SqlParameter("@current_semester_code", SqlDbType.NVarChar) { Value = Request.Form["SemesterCode"].ToString() });

                    SqlDataReader reader = viewOptionalCourse.ExecuteReader();

                    while (reader.Read())
                    {
                        Course course = new Course
                        {
                            courseId = reader.GetInt32(0),
                            name = reader.GetString(1),
                            major = reader.GetString(2),
                            isOffered = reader.GetBoolean(3),
                            creditHours = reader.GetInt32(4),
                            semester = reader.GetInt32(5)
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
        public string major;
        public bool isOffered;
        public int creditHours;
        public int semester;
    }
}
