using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class StudentViewSlotModel : PageModel
    {
        public List<StudentViewSlot> StudentViewSlotList = new List<StudentViewSlot>();
        public int courseId = 1;
        public int instructorId = 2;

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM FN_StudentViewSlot(@CourseID, @InstructorID)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@CourseID", SqlDbType.Int) { Value = courseId });
                        command.Parameters.Add(new SqlParameter("@InstructorID", SqlDbType.Int) { Value = instructorId });

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentViewSlot studentViewSlot = new StudentViewSlot
                                {
                                    SlotId = reader.GetInt32(0),
                                    Location = reader.GetString(1),
                                    Time = reader.GetString(2),
                                    Day = reader.GetString(3),
                                    CourseName = reader.GetString(4),
                                    InstructorName = reader.GetString(5)
                                };

                                StudentViewSlotList.Add(studentViewSlot);
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

    public class StudentViewSlot
    {
        public int SlotId { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }
        public string CourseName { get; set; }
        public string InstructorName { get; set; }
    }
}
