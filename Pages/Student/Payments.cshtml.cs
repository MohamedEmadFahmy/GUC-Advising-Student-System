using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class PaymentsModel : PageModel
    {
        public DateTime Installment_deadline;
        public int? studentId;

        public void OnGet()
        {

            studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                return;
            }
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT dbo.FN_StudentUpcoming_installment(@student_ID) AS Installment_deadline";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@student_ID", SqlDbType.Int) { Value = Convert.ToInt32(studentId) });


                        var result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            Installment_deadline = (DateTime)result;
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

}
