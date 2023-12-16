using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminViewStudentTranscriptModel : PageModel
    {
        public List<StudentsCoursesTranscript> transcriptEntries = new List<StudentsCoursesTranscript>();
        public void OnGet()
        {
            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                String sql = "SELECT * FROM Students_Courses_transcript";
                SqlCommand ViewPayments = new SqlCommand(sql, connection);

                SqlDataReader reader = ViewPayments.ExecuteReader();
                while (reader.Read())
                {
                    StudentsCoursesTranscript transcriptEntry = new StudentsCoursesTranscript
                    {
                        StudentID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        CourseID = reader.GetInt32(3),
                        CourseName = reader.GetString(4),
                        ExamType = reader.GetString(5),
                        Grade = reader.IsDBNull(6) ? "No grade yet" : reader.GetString(6),
                        SemesterCode = reader.GetString(7)
                    };

                    transcriptEntries.Add(transcriptEntry);
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
    public class StudentsCoursesTranscript
    {
        public int StudentID;
        public string FirstName;
        public string LastName;
        public int CourseID;
        public string CourseName;
        public string ExamType;
        public string Grade;
        public string SemesterCode;
    }

}
