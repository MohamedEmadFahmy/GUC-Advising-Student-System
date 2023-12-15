using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Namespace;
using System.Data;
using System.Data.SqlClient;


namespace DataBaseMs3.Pages.Admin
{
    public class ListAdvisorsModel : PageModel
    {
        public List<Advisor> Advisors = new List<Advisor>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Procedures_AdminListAdvisors";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Advisor advisor = new Advisor
                                {
                                    AdvisorID = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Office = reader.GetString(3),
                                    Password = reader.GetString(4)
                                };
                                Advisors.Add(advisor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
    }

    public class Advisor
    {
        public int AdvisorID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }
        public string Password { get; set; }
    }
}
