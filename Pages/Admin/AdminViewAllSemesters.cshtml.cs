using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminViewAllSemestersModel : PageModel
    {
        public List<SemesterOfferedCourse> offeredCourses = new List<SemesterOfferedCourse>();
        public void OnGet()
        {
            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                String sql = "SELECT * FROM Semster_offered_Courses";
                SqlCommand ViewPayments = new SqlCommand(sql, connection);

                SqlDataReader reader = ViewPayments.ExecuteReader();
                while (reader.Read())
                {
                    SemesterOfferedCourse offeredCourse = new SemesterOfferedCourse
                    {
                        CourseID = reader.GetInt32(0),
                        CourseName = reader.GetString(1),
                        SemesterCode = reader.GetString(2)
                    };

                    offeredCourses.Add(offeredCourse);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
    }
    public class SemesterOfferedCourse
    {
        public int CourseID;
        public string CourseName;
        public string SemesterCode;
    }

}
