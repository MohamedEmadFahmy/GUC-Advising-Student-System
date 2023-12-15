using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;



namespace DataBaseMs3.Pages.Admin
{
    public class ListStudentsWithAdvisorsModel : PageModel
    {
        public List<StudentWithAdvisor> StudentsWithAdvisors = new List<StudentWithAdvisor>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "AdminListStudentsWithAdvisors";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentWithAdvisor studentWithAdvisor = new StudentWithAdvisor
                                {
                                    StudentID = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    AdvisorID = reader.GetInt32(3),
                                    AdvisorName = reader.GetString(4)
                                };
                                StudentsWithAdvisors.Add(studentWithAdvisor);
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

    public class StudentWithAdvisor
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AdvisorID { get; set; }
        public string AdvisorName { get; set; }
    }
}
