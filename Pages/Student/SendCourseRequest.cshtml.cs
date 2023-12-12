using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class SendCourseRequestModel : PageModel
    {

        public int studentId = 1;
        public List<Request> Requests = new List<Request>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM Request WHERE student_id = @StudentId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Request request = new Request();
                        request.requestId = reader.GetInt32(reader.GetOrdinal("request_id"));
                        request.type = reader.GetString(reader.GetOrdinal("type"));
                        request.comment = reader.GetString(reader.GetOrdinal("comment"));
                        request.status = reader.GetString(reader.GetOrdinal("status"));
                        request.advisorId = reader.GetInt32(reader.GetOrdinal("advisor_id"));


                        request.creditHours = reader.IsDBNull(reader.GetOrdinal("credit_hours")) ? "None" : reader["credit_hours"].ToString();
                        request.courseId = reader.IsDBNull(reader.GetOrdinal("course_id")) ? "None" : reader["course_id"].ToString();

                        Requests.Add(request);
                    }

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

    public class Request
    {
        public int requestId;
        public string type;
        public string comment;
        public string status;
        public string creditHours;
        public string courseId;
        public int advisorId;
    }
}
