using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class StudentViewGPModel : PageModel
    {
        public List<StudentViewGP> StudentViewGPList = new List<StudentViewGP>();
        public int? studentId;

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
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM FN_StudentViewGP(@student_ID)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@student_ID", SqlDbType.Int) { Value = Convert.ToInt32(studentId) });

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentViewGP studentViewGP = new StudentViewGP
                                {
                                    Student_name = reader.GetString(0),
                                    graduation_Plan_Id = reader.GetInt32(1),
                                    Semester_code = reader.GetString(2),
                                    Semester_credit_hours = reader.GetInt32(3),
                                    expected_grad_date = reader.GetDateTime(4),
                                    advisor_id = reader.GetInt32(5),
                                    Student_Id = reader.GetInt32(6),
                                    Course_id = reader.GetInt32(7),
                                    Course_name = reader.GetString(8),
                                };

                                StudentViewGPList.Add(studentViewGP);
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

    public class StudentViewGP
    {
        public int Student_Id { get; set; }
        public string Student_name { get; set; }
        public int graduation_Plan_Id { get; set; }
        public int Course_id { get; set; }
        public string Course_name { get; set; }
        public string Semester_code { get; set; }
        public DateTime? expected_grad_date { get; set; }
        public int? Semester_credit_hours { get; set; }
        public int advisor_id { get; set; }
    }
}
