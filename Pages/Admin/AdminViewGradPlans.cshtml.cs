using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminViewGradPlansModel : PageModel
    {
        public List<AdvisorsGraduationPlan> graduationPlans = new List<AdvisorsGraduationPlan>();
        public void OnGet()
        {
            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                String sql = "SELECT * FROM Advisors_Graduation_Plan";
                SqlCommand ViewPayments = new SqlCommand(sql, connection);

                SqlDataReader reader = ViewPayments.ExecuteReader();
                while (reader.Read())
                {
                    AdvisorsGraduationPlan plan = new AdvisorsGraduationPlan
                    {
                        PlanID = reader.GetInt32(0),
                        SemesterCode = reader.GetString(1),
                        SemesterCreditHours = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        ExpectedGradDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                        AdvisorID = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                        StudentID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                        AdvisorIDFromView = reader.GetInt32(6),
                        AdvisorName = reader.GetString(7)
                    };

                    graduationPlans.Add(plan);
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
    public class AdvisorsGraduationPlan
    {
        public int PlanID;
        public string SemesterCode;
        public int? SemesterCreditHours;
        public DateTime? ExpectedGradDate;
        public int? AdvisorID;
        public int? StudentID;
        public int AdvisorIDFromView;
        public string AdvisorName;
    }

}

