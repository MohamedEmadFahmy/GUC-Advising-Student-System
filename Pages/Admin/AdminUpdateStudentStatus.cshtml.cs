using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminUpdateStudentStatusModel : PageModel
    {
        public string Message { get; set; } = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();
                String sql = "Procedure_AdminUpdateStudentStatus";
                SqlCommand updateStatus = new SqlCommand(sql, connection);

                updateStatus.CommandType = CommandType.StoredProcedure;
                updateStatus.Parameters.Add(new SqlParameter("@student_id", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["studentid"]) });

                updateStatus.ExecuteNonQuery();
                Message = "Status updated successfully!";
                connection.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
    }
}
