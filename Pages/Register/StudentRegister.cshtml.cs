using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class StudentRegisterModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        public int studentId;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // No need for a Student class, use local variables

            string email = Request.Form["email"];
            string password = Request.Form["password"];
            string firstName = Request.Form["firstName"];
            string lastName = Request.Form["lastName"];
            string faculty = Request.Form["faculty"];
            string major = Request.Form["major"];
            int semester = Convert.ToInt32(Request.Form["semester"]);

            // Save the data to the database
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;" +
                    "Initial Catalog=Advising_System;" +
                    "Integrated Security=True;" +
                    "Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand loginProc = new SqlCommand("Procedures_StudentRegistration", connection))
                    {
                        loginProc.CommandType = CommandType.StoredProcedure;

                        // Add parameters based on the expected parameters of your stored procedure
                        loginProc.Parameters.Add(new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = firstName });
                        loginProc.Parameters.Add(new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = lastName });
                        loginProc.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = password });
                        loginProc.Parameters.Add(new SqlParameter("@faculty", SqlDbType.NVarChar) { Value = faculty });
                        loginProc.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar) { Value = email });
                        loginProc.Parameters.Add(new SqlParameter("@major", SqlDbType.NVarChar) { Value = major });
                        loginProc.Parameters.Add(new SqlParameter("@semester", SqlDbType.Int) { Value = semester });

                        SqlParameter result = new SqlParameter("@Student_id", SqlDbType.Int);
                        result.Direction = ParameterDirection.Output;
                        loginProc.Parameters.Add(result);

                        connection.Open();
                        loginProc.ExecuteNonQuery();

                        studentId = Convert.ToInt32(result.Value);

                        connection.Close();

                        successMessage = "Student Registration Successful. Student ID: " + studentId;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
