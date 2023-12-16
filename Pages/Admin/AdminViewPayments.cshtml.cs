using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace DataBaseMs3.Pages.Admin
{
    public class AdminViewPaymentsModel : PageModel
    {
        public List<studentPayment> StudentPayments = new List<studentPayment>();
        public void OnGet()
        {
            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                String sql = "SELECT * FROM Student_Payment";
                SqlCommand ViewPayments = new SqlCommand(sql, connection);
                    
                SqlDataReader reader = ViewPayments.ExecuteReader();    
                while (reader.Read())
                {
                    studentPayment payment = new studentPayment();
                    payment.studentID = reader.GetInt32(0);
                    payment.studentName = reader.GetString(1) + " " + reader.GetString(2);
                    payment.paymentID = reader.GetInt32(3);
                    payment.amount = reader.GetInt32(4);
                    payment.startDate = reader.GetDateTime(5);
                    payment.deadline = reader.GetDateTime(6);
                    payment.n_installments = reader.GetInt32(7);
                    payment.fund_percentage = reader.GetDecimal(8);
                    payment.status = reader.GetString(9);
                    payment.student_ID = reader.GetInt32(10);
                    payment.semesterCode = reader.GetString(11);
                    StudentPayments.Add(payment);
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
    public class studentPayment {
        public int studentID;
        public string studentName;
        public int paymentID;
        public int amount;
        public DateTime startDate;
        public DateTime deadline;
        public int n_installments;
        public decimal fund_percentage;
        public string status;
        public int student_ID;
        public string semesterCode;
    }
}
