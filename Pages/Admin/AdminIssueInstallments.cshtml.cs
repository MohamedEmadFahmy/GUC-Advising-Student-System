using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminIssueInstallmentsModel : PageModel
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
                String sql = "Procedures_AdminIssueInstallment";
                SqlCommand IssueInstallment = new SqlCommand(sql, connection);

                IssueInstallment.CommandType = CommandType.StoredProcedure;
                IssueInstallment.Parameters.Add(new SqlParameter("@payment_id", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["paymentid"])) });

                IssueInstallment.ExecuteNonQuery();
                Message = "Installments Issued Successfully!";
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
