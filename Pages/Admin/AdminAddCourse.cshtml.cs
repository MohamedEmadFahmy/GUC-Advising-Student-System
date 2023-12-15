using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminAddCourseModel : PageModel
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
                    string sql = "Procedures_AdminAddingCourse";
                    SqlCommand AddCourse = new SqlCommand(sql, connection);

                    AddCourse.CommandType = CommandType.StoredProcedure;
                    AddCourse.Parameters.Add(new SqlParameter("@major", SqlDbType.VarChar) { Value = (Request.Form["major"]).ToString() });
                    AddCourse.Parameters.Add(new SqlParameter("@semester", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["semester"]) });
                    AddCourse.Parameters.Add(new SqlParameter("@credit_hours", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["credit_hours"]) });
                    AddCourse.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar) { Value = (Request.Form["name"]).ToString() });

                    // Adjustment for parsing checkbox value to boolean
                    bool isOffered = Request.Form["is_offered"] == "on" ? true : false;
                    AddCourse.Parameters.Add(new SqlParameter("@is_offered", SqlDbType.Bit) { Value = isOffered });

                    AddCourse.ExecuteNonQuery();
                    Message = "Course Added Successfully!";
                    connection.Close();
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
