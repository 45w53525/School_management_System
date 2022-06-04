using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace School_management_System.Models
{
    public class Subjects
    {
        [Key]
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public int SubjectFees { get; set; }





    }
}