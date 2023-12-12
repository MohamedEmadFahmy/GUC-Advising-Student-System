using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminViewActiveStudentsModel : PageModel
    {
        public List<Student> ActiveStudents = new List<Student>();
        public void OnGet()
        {
            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                String sql = "SELECT * FROM view_Students";
                SqlCommand ViewPayments = new SqlCommand(sql, connection);

                SqlDataReader reader = ViewPayments.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student
                    {
                        StudentID = reader.GetInt32(0),
                        Name = reader.GetString(1) + " " + reader.GetString(2),
                        Password = reader.GetString(3),
                        GPA = reader.GetDecimal(4),
                        Faculty = reader.GetString(5),
                        Email = reader.GetString(6),
                        Major = reader.GetString(7),
                        FinancialStatus = reader.IsDBNull(8) ? (bool?)null : reader.GetBoolean(8),
                        Semester = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9),
                        AcquiredHours = reader.IsDBNull(10) ? (int?)null : reader.GetInt32(10),
                        AssignedHours = reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11),
                        AdvisorID = reader.IsDBNull(12) ? (int?)null : reader.GetInt32(12),
                    };
                    ActiveStudents.Add(student);
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
    public class Student
    {
        public int StudentID;
        public string Name;
        public string Password;
        public decimal GPA;
        public string Faculty;
        public string Email;
        public string Major;
        public bool? FinancialStatus;
        public int? Semester;
        public int? AcquiredHours;
        public int? AssignedHours;
        public int? AdvisorID;
    }

}
