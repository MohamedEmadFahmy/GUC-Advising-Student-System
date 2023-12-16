using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminLinkStudentToAdvisorModel : PageModel
    {
        public string Message { get; set; } = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Procedures_AdminLinkStudentToAdvisor";
                    SqlCommand linkStudentToAdvisor = new SqlCommand(sql, connection);
                    linkStudentToAdvisor.CommandType = CommandType.StoredProcedure;

                    int? studentId = !string.IsNullOrEmpty(Request.Form["studentID"]) ? Convert.ToInt32(Request.Form["studentID"]) : (int?)null;
                    int? advisorId = !string.IsNullOrEmpty(Request.Form["advisorID"]) ? Convert.ToInt32(Request.Form["advisorID"]) : (int?)null;

                    if (studentId == null || advisorId == null)
                    {
                        Console.WriteLine("One of the inputs is null");
                        return;
                    }

                    linkStudentToAdvisor.Parameters.Add(new SqlParameter("@studentID", SqlDbType.Int) { Value = studentId });
                    linkStudentToAdvisor.Parameters.Add(new SqlParameter("@advisorID", SqlDbType.Int) { Value = advisorId });

                    linkStudentToAdvisor.ExecuteNonQuery();
                    Message = "Student linked to the course successfully!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
    }
}
