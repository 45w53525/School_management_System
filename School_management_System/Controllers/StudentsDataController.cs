using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using School_management_System.Models;
using System.Diagnostics;

namespace School_management_System.Controllers
{
    public class StudentsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StudentsData/ListStudents
        [HttpGet]
        public IEnumerable<StudentDto> ListStudents()
        {
            List<Students> Students = db.Student.ToList();
            List<StudentDto> StudentDtos = new List<StudentDto>();
            Students.ForEach(a => StudentDtos.Add(new StudentDto() { 
            


                StudentId = a.StudentId,
                FirstName = a.FirstName,
                LastName = a.LastName,





            }));
            
          return StudentDtos;
        
        }





    

        // GET: api/StudentsData/Findstudents/2
        [ResponseType(typeof(Students))]
        [HttpGet]
        public IHttpActionResult FindStudents(int id)
        {
            Students Students = db.Student.Find(id);
            StudentDto StudentsDto = new StudentDto()
            {
                StudentId = Students.StudentId,
                FirstName = Students.FirstName,
                LastName = Students.LastName,


            };

            if (Students == null)
            {
                return NotFound();
            }

            return Ok(StudentsDto);
        }




        // Post: api/StudentsData/updateStudents/2
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateStudents(int id, Students students)
        {
            Debug.WriteLine("I have reached the update animal method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != students.StudentId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + students.StudentId);
                Debug.WriteLine("POST parameter" + students.FirstName);
                Debug.WriteLine("POST parameter " + students.LastName);
                return BadRequest();
            }

            db.Entry(students).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StudentsData/AddStudents
        [ResponseType(typeof(Students))]
        [HttpPost]
        public IHttpActionResult AddStudents(Students students)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Student.Add(students);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = students.StudentId }, students);
        }

        // DELETE: api/StudentsData/DeleteStudents/5
        [ResponseType(typeof(Students))]
        [HttpPost]
        public IHttpActionResult DeleteStudents(int id)
        {
            Students students = db.Student.Find(id);
            if (students == null)
            {
                return NotFound();
            }

            db.Student.Remove(students);
            db.SaveChanges();

            return Ok(students);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentsExists(int id)
        {
            return db.Student.Count(e => e.StudentId == id) > 0;
        }
    }
}