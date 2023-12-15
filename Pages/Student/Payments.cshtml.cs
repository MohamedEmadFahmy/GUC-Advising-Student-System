using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class PaymentsModel : PageModel
    {
        public string Installment_deadline = "";
        public int stud_id = 2;

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT dbo.FN_StudentUpcoming_installment(@student_ID)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@student_ID", SqlDbType.Int) { Value = Convert.ToInt32(stud_id) });


                        Installment_deadline = command.ExecuteScalar() + "";

                        // if (result != null && result != DBNull.Value)
                        // {
                        // }
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
