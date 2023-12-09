using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class StudentRegisterModel : PageModel
    {
        public Student s = new Student();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {

            s.email = Request.Form["firstName"];
            s.password = Request.Form["password"];
            s.firstName = Request.Form["firstName"];
            s.lastName = Request.Form["lastName"];
            s.faculty = Request.Form["faculty"];
            s.major = Request.Form["major"];
            int semester;
            if (int.TryParse(Request.Form["semester"], out semester))
            {
                s.semester = semester;
            }
            else
            {
                errorMessage = "invalid semester";
                return;
            }


            // save the data to the database

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;" +
                    "Initial Catalog=Advising_System;" +
                    "Integrated Security=True;" +
                    "Encrypt=False";


                SqlConnection connection = new SqlConnection(connectionString);



                SqlCommand loginProc = new SqlCommand("Procedures_StudentRegistration ", connection);


                loginProc.CommandType = CommandType.StoredProcedure;

                // Add parameters based on the expected parameters of your stored procedure
                loginProc.Parameters.Add(new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = s.firstName });
                loginProc.Parameters.Add(new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = s.lastName });
                loginProc.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = s.password });
                loginProc.Parameters.Add(new SqlParameter("@faculty", SqlDbType.NVarChar) { Value = s.faculty });
                loginProc.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar) { Value = s.email });
                loginProc.Parameters.Add(new SqlParameter("@major", SqlDbType.NVarChar) { Value = s.major });
                loginProc.Parameters.Add(new SqlParameter("@semester", SqlDbType.Int) { Value = s.semester });

                SqlParameter result = new SqlParameter("@Student_id", SqlDbType.Int);
                result.Direction = ParameterDirection.Output;
                loginProc.Parameters.Add(result);


                connection.Open();
                loginProc.ExecuteNonQuery();

                s = new Student();

                s.id = Convert.ToInt32(result.Value);

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

    public class Student
    {
        public string email;
        public string password;
        public string firstName;
        public string lastName;
        public string faculty;
        public string major;
        public int semester;
        public int id;
    }

}
