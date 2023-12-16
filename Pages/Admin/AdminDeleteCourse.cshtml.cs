using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminModel : PageModel
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
                String sql = "Procedures_AdminDeleteCourse";
                SqlCommand DeleteCourse = new SqlCommand(sql, connection);

                DeleteCourse.CommandType = CommandType.StoredProcedure;
                DeleteCourse.Parameters.Add(new SqlParameter("@courseID", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["coursename"]) });

                DeleteCourse.ExecuteNonQuery();
                Message = "Course Deleted successfully!";
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
