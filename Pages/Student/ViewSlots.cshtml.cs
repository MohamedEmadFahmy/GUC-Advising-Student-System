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
        public List<CoursesSlotsInstructor> StudentViewSlotList = new List<CoursesSlotsInstructor>();
        public int courseId = 1;
        public int instructorId = 1;

        public bool isPosted = false;

        public IActionResult OnGet()
        {
            isPosted = false;

            if (HttpContext.Session.GetInt32("student_id") == null)
            {
                return RedirectToPage("../Login/Login");
            }

            return Page();

        }
        public void OnPost()
        {
            isPosted = true;
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM FN_StudentViewSlot(@CourseID, @InstructorID)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@CourseID", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["courseID"]) });
                        command.Parameters.Add(new SqlParameter("@InstructorID", SqlDbType.Int) { Value = Convert.ToInt32(Request.Form["InstructorID"]) });

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CoursesSlotsInstructor coursesSlotsInstructor = new CoursesSlotsInstructor
                                {
                                    course_id = reader.GetInt32(0),
                                    courseName = reader.GetString(1),
                                    slot_id = reader.GetInt32(2),
                                    day = reader.GetString(3),
                                    time = reader.GetString(4),
                                    location = reader.GetString(5),
                                    constructorid = reader.GetInt32(6),
                                    instructorid = reader.GetInt32(7),
                                    instructor = reader.GetString(8),
                                };
                                StudentViewSlotList.Add(coursesSlotsInstructor);
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

    // public class StudentViewSlot
    // {
    //     public int SlotId { get; set; }
    //     public string Location { get; set; }
    //     public string Time { get; set; }
    //     public string Day { get; set; }
    //     public string CourseName { get; set; }
    //     public string InstructorName { get; set; }
    // }
}
