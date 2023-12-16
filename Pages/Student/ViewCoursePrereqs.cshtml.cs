using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class ViewCoursePrerequisitesModel : PageModel
    {
        public List<CoursePrerequisites> CoursePrerequisitesList = new List<CoursePrerequisites>();

        public void OnGet()
        {

            int? studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                Response.Redirect("../Login/Login");
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM view_Course_prerequisites";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CoursePrerequisites coursePrerequisites = new CoursePrerequisites
                                {
                                    course_id = reader.GetInt32(0),
                                    name = reader.GetString(1),
                                    major = reader.GetString(2),
                                    is_offered = reader.GetBoolean(3),
                                    credit_hours = reader.GetInt32(4),
                                    semester = reader.GetInt32(5),
                                    prereq_courseid = reader.GetInt32(6),
                                    prereq_coursename = reader.GetString(7)
                                };

                                CoursePrerequisitesList.Add(coursePrerequisites);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }

    }

    public class CoursePrerequisites
    {
        public int course_id { get; set; }
        public string name { get; set; }
        public string major { get; set; }
        public bool is_offered { get; set; }
        public int credit_hours { get; set; }
        public int semester { get; set; }
        public int prereq_courseid { get; set; }
        public string prereq_coursename { get; set; }
    }
}
