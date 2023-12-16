using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class StudentModel : PageModel
    {
        private int? studentId;

        public Student currentStudent = new Student();
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

                    String sql = "select * from Student where student_id = @StudentID";

                    SqlCommand cmd = new SqlCommand(sql, connection);


                    cmd.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = studentId });

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        currentStudent = new Student
                        {
                            StudentId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Password = reader.GetString(3),
                            GPA = reader.GetDecimal(4),
                            Faculty = reader.GetString(5),
                            Email = reader.GetString(6),
                            Major = reader.GetString(7),
                            FinancialStatus = reader.GetBoolean(8),
                            Semester = reader.GetInt32(9),
                            AcquiredHours = reader.GetInt32(10),
                            AssignedHours = reader.GetInt32(11),
                            AdvisorId = reader.GetInt32(12)
                        };

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
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public decimal GPA { get; set; }
        public string Faculty { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public bool FinancialStatus { get; set; }
        public int Semester { get; set; }
        public int AcquiredHours { get; set; }
        public int AssignedHours { get; set; }
        public int AdvisorId { get; set; }
    }

}
