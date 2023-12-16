using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyApp.Namespace.Admin
{
    public class AdminAddingSemesterModel : PageModel
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
                using SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();
                string sql = "AdminAddingSemester";
                using SqlCommand addSemester = new SqlCommand(sql, connection);
                addSemester.CommandType = CommandType.StoredProcedure;

                // here i convert StringValues to appropriate data types
                string startDateStr = Request.Form["start_date"];
                string endDateStr = Request.Form["end_date"];
                string semesterCode = Request.Form["semester_code"];

                if (DateTime.TryParse(startDateStr, out DateTime startDate) &&
                    DateTime.TryParse(endDateStr, out DateTime endDate) &&
                    !string.IsNullOrEmpty(semesterCode))
                {
                    addSemester.Parameters.AddWithValue("@start_date", startDate);
                    addSemester.Parameters.AddWithValue("@end_date", endDate);
                    addSemester.Parameters.AddWithValue("@semester_code", semesterCode);

                    addSemester.ExecuteNonQuery();
                    Message = "Semester Added Successfully!";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                // throw;
                Message = "error";
            }
        }
    }
}
