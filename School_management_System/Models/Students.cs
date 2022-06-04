﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace School_management_System.Models
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
    public class StudentDto
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


    }










}