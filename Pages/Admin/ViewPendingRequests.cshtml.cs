using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminListPendingRequestsModel : PageModel
    {
        public List<Request> PendingRequests = new List<Request>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                String sql = "SELECT * FROM all_Pending_Requests";
                SqlCommand viewPendingRequests = new SqlCommand(sql, connection);

                SqlDataReader reader = viewPendingRequests.ExecuteReader();
                while (reader.Read())
                {
                    Request request = new Request
                    {
                        RequestID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Type = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Comment = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        Status = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        CreditHours = reader.IsDBNull(4) ? "None" : reader.GetInt32(4).ToString(),
                        CourseID = reader.IsDBNull(5) ? "None" : reader.GetInt32(5).ToString(),
                        StudentID = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                        AdvisorID = reader.IsDBNull(7) ? 0 : reader.GetInt32(7)
                    };
                    PendingRequests.Add(request);
                }
                connection.Close();
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
        public int RequestID;
        public string Type;
        public string Comment;
        public string Status;
        public string CreditHours;
        public string CourseID;
        public int StudentID;
        public int AdvisorID;
    }
}
