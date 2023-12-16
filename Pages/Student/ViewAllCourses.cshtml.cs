using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class ListCoursesSlotsInstructorsModel : PageModel
    {
        public List<CoursesSlotsInstructor> CoursesSlotsInstructors = new List<CoursesSlotsInstructor>();

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
                    string sql = "SELECT * FROM Courses_Slots_Instructor";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Use CommandType.Text for a SELECT query
                        command.CommandType = CommandType.Text;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CoursesSlotsInstructor coursesSlotsInstructor = new CoursesSlotsInstructor
                                {
                                    course_id = reader.GetInt32(0),
                                    courseName = reader.GetString(1),
                                    slot_id = reader.GetInt32(2),
                                    day = reader.GetString(3),
                                    time = reader.GetString(4),
                                    location = reader.GetString(5),
                                    constructorid = reader.GetInt32(6),
                                    instructorid = reader.GetInt32(7),
                                    instructor = reader.GetString(8),
                                };
                                CoursesSlotsInstructors.Add(coursesSlotsInstructor);
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

    public class CoursesSlotsInstructor
    {
        public int course_id { get; set; }
        public string courseName { get; set; }
        public int slot_id { get; set; }
        public string day { get; set; }
        public string time { get; set; }
        public string location { get; set; }
        public int constructorid { get; set; }
        public string instructor { get; set; }
        public int instructorid { get; set; }
    }
}
