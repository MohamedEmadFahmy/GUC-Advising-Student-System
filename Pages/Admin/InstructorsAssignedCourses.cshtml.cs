using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class InstructorsAssignedCourses : PageModel
{
    public List<InstructorCourses> InstructorsCoursesList { get; set; } = new List<InstructorCourses>();

    public void OnGet()
    {
        try
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Instructors_AssignedCourses";
                using (SqlCommand viewInstructorsCourses = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = viewInstructorsCourses.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InstructorCourses instructorCourses = new InstructorCourses
                            {
                                InstructorID = reader.GetInt32(0),
                                InstructorName = reader.GetString(1),
                                CourseID = reader.GetInt32(2),
                                CourseName = reader.GetString(3)
                            };
                            InstructorsCoursesList.Add(instructorCourses);
                        }
                    }
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

public class InstructorCourses
{
    public int InstructorID { get; set; }
    public string InstructorName { get; set; }
    public int CourseID { get; set; }
    public string CourseName { get; set; }
}
