using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Workshop.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Age { get; set; } // Keep as string to match DB values

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True;";

        // ✅ Retrieve Data (By ID)
        public List<Student> getData(string id)
        {
            List<Student> lstStu = new List<Student>();
            string query = "SELECT * FROM Student";

            if (!string.IsNullOrWhiteSpace(id))
            {
                query += " WHERE Id = @id";
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                }

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lstStu.Add(new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Age = reader["Age"].ToString()
                        });
                    }
                }
            }
            return lstStu;
        }

        // ✅ Insert Student Data
        public bool insert(Student student)
        {
            if (!string.IsNullOrWhiteSpace(student.Name) && !string.IsNullOrWhiteSpace(student.Email) && !string.IsNullOrWhiteSpace(student.Age))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Student (Name, Email, Age) VALUES (@name, @email, @age)", con))
                {
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@email", student.Email);
                    cmd.Parameters.AddWithValue("@age", student.Age);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
            return false;
        }

        // ✅ Update Student Data
        public bool update(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE Student SET Name=@name, Email=@email, Age=@age WHERE Id=@id", con))
            {
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@age", student.Age);
                cmd.Parameters.AddWithValue("@id", student.Id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;  // Returns true if at least 1 row is updated
            }
        }


        // ✅ Delete Student Data
        public bool delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE Id = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        // ✅ Get Student by ID
        public Student GetStudentById(int id)
        {
            string query = "SELECT * FROM Student WHERE Id=@Id"; // Ensure table name is correct

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Age = reader["Age"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        // ✅ Get All Students
        public List<Student> GetAllStudents()
        {
            List<Student> lststu = new List<Student>();
            string query = "SELECT * FROM Student";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lststu.Add(new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Age = reader["Age"].ToString()
                        });
                    }
                }
            }
            return lststu;
        }
    }
}
