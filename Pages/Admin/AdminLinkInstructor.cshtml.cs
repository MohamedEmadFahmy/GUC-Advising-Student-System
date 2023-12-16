using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace DataBaseMs3.Pages.Admin
{
    public class AdminLinkInstructorModel : PageModel
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
                    string sql = "Procedures_AdminLinkInstructor";
                    SqlCommand linkInstructor = new SqlCommand(sql, connection);
                    linkInstructor.CommandType = CommandType.StoredProcedure;

                    int? courseId = !string.IsNullOrEmpty(Request.Form["courseID"]) ? Convert.ToInt32(Request.Form["courseID"]) : (int?)null;
                    int? instructorId = !string.IsNullOrEmpty(Request.Form["instructorID"]) ? Convert.ToInt32(Request.Form["instructorID"]) : (int?)null;
                    int? slotId = !string.IsNullOrEmpty(Request.Form["slotID"]) ? Convert.ToInt32(Request.Form["slotID"]) : (int?)null;

                    if (courseId == null || instructorId == null || slotId == null)
                    {
                        Console.WriteLine("One of the inputs is null");
                        return;
                    }

                    linkInstructor.Parameters.Add(new SqlParameter("@cours_id", SqlDbType.Int) { Value = courseId });
                    linkInstructor.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int) { Value = instructorId });
                    linkInstructor.Parameters.Add(new SqlParameter("@slot_id", SqlDbType.Int) { Value = slotId });

                    linkInstructor.ExecuteNonQuery();
                    Message = "Instructor linked to the course in the slot successfully!";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception: " + ex.ToString());
                Message = "Error";
            }
        }
    }
}

