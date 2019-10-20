using CRUD.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Student
    {

        //private StudentContext context;
        public int StudentId { get; set; }
        [Required(ErrorMessage = "First name is required!")]
        public string First_name { get; set; }

        [Required(ErrorMessage ="Middle name is required!")]
        public string Middle_name { get; set; }
        
        [Required(ErrorMessage ="Last name is required!")]
        public string Last_name { get; set; }

        public Student(int studentId, string first_name, string middle_name, string last_name )
        {
            this.StudentId = studentId;
            this.First_name = first_name;
            this.Middle_name = middle_name;
            this.Last_name = last_name;
        }

        public Student()
        {

        }

    }
}
