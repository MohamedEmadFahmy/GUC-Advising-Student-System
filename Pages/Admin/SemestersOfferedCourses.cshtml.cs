// Add these using statements at the top if not already present
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class SemestersOfferedCoursesModel : PageModel
{
	public List<SemesterOfferedCourses> SemestersWithCourses { get; set; } = new List<SemesterOfferedCourses>();

	public void OnGet()
	{
		try
		{
			string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Advising_System;Integrated Security=True";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				string sql = "SELECT * FROM Semster_offered_Courses";
				using (SqlCommand viewSemesters = new SqlCommand(sql, connection))
				{
					using (SqlDataReader reader = viewSemesters.ExecuteReader())
					{
						while (reader.Read())
						{
							SemesterOfferedCourses semesterCourse = new SemesterOfferedCourses
							{
								CourseID = reader.GetInt32(0),
								CourseName = reader.GetString(1),
								SemesterCode = reader.GetString(2)
							};
							SemestersWithCourses.Add(semesterCourse);
						}
					}
				}
				connection.Close();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Exception: " + ex.ToString());
			throw;
		}
	}
}

public class SemesterOfferedCourses
{
	public int CourseID { get; set; }
	public string CourseName { get; set; }
	public string SemesterCode { get; set; }
}
