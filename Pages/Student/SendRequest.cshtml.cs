using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class SendCourseRequestModel : PageModel
    {

        public int? studentId;
        public List<Request> Requests = new List<Request>();

        public bool isCourseRequest = true;
        public void OnGet()
        {
            studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                Response.Redirect("../Login/Login");
                return;
            }

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


        public void OnPost()
        {
            studentId = HttpContext.Session.GetInt32("student_id");

            if (studentId == null)
            {
                return;
            }
            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                if (string.IsNullOrEmpty(Request.Form["CourseID"].ToString()) && !string.IsNullOrEmpty(Request.Form["CreditHours"].ToString()))
                {
                    isCourseRequest = false;
                }

                string sql = isCourseRequest ? "Procedures_StudentSendingCourseRequest" : "Procedures_StudentSendingCHRequest";
                SqlCommand sendRequest = new SqlCommand(sql, connection);
                sendRequest.CommandType = CommandType.StoredProcedure;


                sendRequest.Parameters.Add(new SqlParameter("@comment", SqlDbType.NVarChar) { Value = Request.Form["Comment"].ToString() });

                sendRequest.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.NVarChar) { Value = studentId });

                if (isCourseRequest)
                {

                    sendRequest.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar) { Value = "course" });
                    // sendRequest.Parameters.Add(new SqlParameter("@courseID", SqlDbType.Int) { Value = Convert.ToInt32("1") });
                    sendRequest.Parameters.Add(new SqlParameter("@courseID", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["CourseID"]) });


                }
                else
                {
                    sendRequest.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar) { Value = "credit_hours" });
                    // sendRequest.Parameters.Add(new SqlParameter("@credit_hours", SqlDbType.Int) { Value = Convert.ToInt32("15") });
                    sendRequest.Parameters.Add(new SqlParameter("@credit_hours", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["CreditHours"]) });
                }


                sendRequest.ExecuteNonQuery();
                connection.Close();

                OnGet();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                // throw;
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
