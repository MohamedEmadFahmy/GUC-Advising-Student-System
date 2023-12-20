using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class StudentModel : PageModel
    {
        private int? studentId;
        public string message = "";
        public string errormessage = "";
        public bool isPosted;

        public Student currentStudent = new Student();
        public void OnGet()
        {
            message = "";
            errormessage = "";

            isPosted = false;

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
                            StudentId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            FirstName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            LastName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            Password = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            GPA = reader.IsDBNull(4) ? 0.0m : reader.GetDecimal(4),
                            Faculty = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                            Email = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                            Major = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                            FinancialStatus = reader.IsDBNull(7) ? false : reader.GetBoolean(8),
                            Semester = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                            AcquiredHours = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                            AssignedHours = reader.IsDBNull(11) ? 0 : reader.GetInt32(11),
                            AdvisorId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12)
                        };

                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    
        public void OnPost()
        {
            isPosted = true;
            studentId = HttpContext.Session.GetInt32("student_id");
            if (studentId == null)
                return;
            try
                {

                    String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                    SqlConnection connection = new SqlConnection(connectionString);

                    connection.Open();
                    String sql = "Procedures_StudentaddMobile";
                    SqlCommand ChooseExam = new SqlCommand(sql, connection);

                    ChooseExam.CommandType = CommandType.StoredProcedure;

                    ChooseExam.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int) { Value = studentId });

                    ChooseExam.Parameters.Add(new SqlParameter("@mobile_number", SqlDbType.NVarChar) { Value = Request.Form["phone"].ToString()});
                    


                    ChooseExam.ExecuteNonQuery();

                    connection.Close();
                    


                }
            catch (Exception)
            {
                // Console.WriteLine("Exception: " + ex.ToString());
                errormessage = "Error adding Number";
                //throw;
            }
            message = "Number added successfully";
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
