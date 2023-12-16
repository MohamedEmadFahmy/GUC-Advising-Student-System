using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class ListMakeupExamsModel : PageModel
    {
        public List<MakeupExam> MakeupExams = new List<MakeupExam>();

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
                    string sql = "SELECT * FROM Courses_MakeupExams";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MakeupExam makeupExam = new MakeupExam
                                {
                                    examid = reader.GetInt32(0),
                                    date = reader.GetDateTime(1),
                                    type = reader.GetString(2),
                                    courseid = reader.GetInt32(3),
                                    name = reader.GetString(4),
                                    semester = reader.GetInt32(5)
                                };
                                MakeupExams.Add(makeupExam);
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

    public class MakeupExam
    {
        public int examid { get; set; }
        public DateTime date { get; set; }
        public string type { get; set; }
        public int courseid { get; set; }
        public string name { get; set; }
        public int semester { get; set; }
    }
}
