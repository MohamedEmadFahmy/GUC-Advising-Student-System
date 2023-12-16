using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminAddExamModel : PageModel
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
                String sql = "Procedures_AdminAddExam";
                SqlCommand AddExam = new SqlCommand(sql, connection);

                AddExam.CommandType = CommandType.StoredProcedure;
                AddExam.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar) { Value = (Request.Form["type"]).ToString() });
                AddExam.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime) { Value = DateTime.Parse((Request.Form["date"])) });
                AddExam.Parameters.Add(new SqlParameter("@courseID", SqlDbType.Int) { Value = Convert.ToInt32((Request.Form["courseID"])) });
                AddExam.ExecuteNonQuery();
                Message = "Exam Added Successfully!";
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
