using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Milestone_3.Pages
{
    public class LoginModel : PageModel
    {
        public List<Instructor> Instructors = new List<Instructor>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;" +
                    "Initial Catalog=Advising_System;" +
                    "Integrated Security=True;" +
                    "Encrypt=False";


                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();


                String sql = "SELECT * FROM Instructor";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Instructor instructor = new Instructor();
                    instructor.id = "" + reader.GetInt32(0);
                    instructor.name = reader.GetString(1);
                    instructor.email = reader.GetString(2);
                    instructor.faculty = reader.GetString(3);
                    instructor.office = reader.GetString(4);


                    Instructors.Add(instructor);
                }

                connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:  " + e.ToString());
            }

        }
    }




    public class Instructor
    {
        public string id;
        public string name;
        public string email;
        public string faculty;
        public string office;
    }
}
