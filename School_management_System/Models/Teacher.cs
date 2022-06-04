using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace School_management_System.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeachingSubject1 { get; set; }



    }
}