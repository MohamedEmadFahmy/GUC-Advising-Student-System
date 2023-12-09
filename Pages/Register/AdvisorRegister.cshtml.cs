using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class AdvisorRegisterModel : PageModel
    {
        public Advisor a = new Advisor();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            a.name = Request.Form["name"];
            a.password = Request.Form["password"];
            a.email = Request.Form["email"];
            a.office = Request.Form["office"];

            // save the data to the database

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;" +
                    "Initial Catalog=Advising_System;" +
                    "Integrated Security=True;" +
                    "Encrypt=False";


                SqlConnection connection = new SqlConnection(connectionString);



                SqlCommand loginProc = new SqlCommand("Procedures_AdvisorRegistration ", connection);


                loginProc.CommandType = CommandType.StoredProcedure;

                // Add parameters based on the expected parameters of your stored procedure
                loginProc.Parameters.Add(new SqlParameter("@advisor_name", SqlDbType.NVarChar) { Value = a.name });
                loginProc.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = a.password });
                loginProc.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar) { Value = a.email });
                loginProc.Parameters.Add(new SqlParameter("@office", SqlDbType.NVarChar) { Value = a.office });

                SqlParameter result = new SqlParameter("@Advisor_id", SqlDbType.Int);
                result.Direction = ParameterDirection.Output;
                loginProc.Parameters.Add(result);


                connection.Open();
                loginProc.ExecuteNonQuery();

                a = new Advisor();

                a.id = Convert.ToInt32(result.Value);

                connection.Close();



            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            successMessage = "Student Registration Successful";


        }
    }
    public class Advisor
    {
        public string name;
        public string password;
        public string email;
        public string office;
        public int id;
    }
}
