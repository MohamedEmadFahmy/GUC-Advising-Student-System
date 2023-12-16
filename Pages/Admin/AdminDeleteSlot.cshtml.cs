using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminDeleteSlotModel : PageModel
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
                String sql = "Procedures_AdminDeleteSlots";
                SqlCommand DeleteSlot = new SqlCommand(sql, connection);

                DeleteSlot.CommandType = CommandType.StoredProcedure;
                DeleteSlot.Parameters.Add(new SqlParameter("@current_semester", SqlDbType.NVarChar) { Value = (Request.Form["currentsemester"]).ToString() });

                DeleteSlot.ExecuteNonQuery();
                Message = "Slot Deleted successfully!";
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
