using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Models;
using MySql.Data.MySqlClient;

namespace CRUD.Data
{
    public class StudentContext
    {
        public string ConnectionString { get; set; }

        public StudentContext()
        {
            this.ConnectionString = "server=localhost;Database=CRUD;user=root;password=iamsuperman";
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (MySqlConnection conn = GetConnection()){
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM students";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student(
                                studentId: Convert.ToInt32(reader["studentId"]),
                                first_name: reader["first_name"].ToString(),
                                middle_name: reader["middle_name"].ToString(),
                                last_name: reader["last_name"].ToString()
                                ));
                        }
                    }

                    cmd.Dispose();

                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            Console.WriteLine("Cannot connect to database");
                            break;

                        case 1045:
                            Console.Write("Wrong username or password");
                            break;

                    }
                }
            }

            return students;

        }

        public Student GetStudent(int id)
        {

            Student student = null;
            using (MySqlConnection conn = GetConnection())
            {
                    string query = "SELECT * FROM students WHERE studentId LIKE @studentId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@studentId", id);

                    //Debug.WriteLine(cmd.ToString());

                    conn.Open();

                try { 
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student = new Student(
                                studentId: Convert.ToInt32(reader["studentId"]),
                                first_name: reader["first_name"].ToString(),
                                middle_name: reader["middle_name"].ToString(),
                                last_name: reader["last_name"].ToString()
                                );
                        }
                    }
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                    Debug.WriteLine("Error in GetStudent");
                }

                cmd.Dispose();
                conn.Close();
            }
            return student;

        }
        
        public bool CreateStudent(Student student)
        {
            bool success = false;

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                try
                {
                    string query = "INSERT INTO students (first_name, middle_name, last_name) VALUES (@first_name, @middle_name, @last_name)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@first_name", student.First_name);
                    cmd.Parameters.AddWithValue("@middle_name", student.Middle_name);
                    cmd.Parameters.AddWithValue("@last_name", student.Last_name);

                    int i = cmd.ExecuteNonQuery();
                    success = (i >= 1);


                    cmd.Dispose();
             
                }catch(Exception e)
                {
                    Console.WriteLine(e);
                }

                conn.Close();

            }
            
            
            return success;
        }

        public bool UpdateStudent(Student student)
        {
            bool success = false;

            using (MySqlConnection conn = GetConnection())
            {
                string query = "UPDATE students SET " +
                    "first_name  = @first_name, " +
                    "middle_name = @middle_name, " +
                    "last_name   = @last_name " +
                    "WHERE studentId = @studentId";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@first_name", student.First_name);
                cmd.Parameters.AddWithValue("@middle_name", student.Middle_name);
                cmd.Parameters.AddWithValue("@last_name", student.Last_name);
                cmd.Parameters.AddWithValue("@studentId", student.StudentId);

                conn.Open();

                int i = 0;
                try
                {
                    i = cmd.ExecuteNonQuery();
                }catch(Exception e)
                {
                    Debug.WriteLine(e);
                }

                success = (i > 0);

                cmd.Dispose();
                conn.Close();
            }

           
            return success;
        }

        public bool DeleteStudent(int studentId)
        {
            bool success = false;

            using (MySqlConnection conn = GetConnection())
            {
                string query = "DELETE FROM students WHERE studentId = @studentId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);

                conn.Open();

                int i = 0;

                try
                {
                    i = cmd.ExecuteNonQuery();
                }catch(Exception e)
                {
                    Debug.WriteLine(e);
                }

                success = (i > 0);
                cmd.Dispose();
                conn.Close();

            }
            return success;
        }
    }
}
