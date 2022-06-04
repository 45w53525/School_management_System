using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace School_management_System.Models
{
    public class Services
    {
        [Key]
        public int ServiceId { get; set; }




        [ForeignKey("Students")]

        public int StudentId { get; set; }

        public virtual Students Students { get; set; }






        [ForeignKey("Subjects")]

        public int SubjectId { get; set; }

        public virtual Subjects Subjects { get; set; }






        [ForeignKey("Teachers")]

        public int TeacherId { get; set; }

        public virtual Teacher Teachers { get; set; }






    }
}