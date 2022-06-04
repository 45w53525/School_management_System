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

namespace School_management_System.Controllers
{
    public class StudentsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StudentsData/ListStudent
        [HttpGet]
        public IEnumerable<StudentDto> ListStudent()
        {
            List<Students> Students = db.Student.ToList();
            List<StudentDto> studentDtos = new List<StudentDto>();
            Students.ForEach(a => StudentDto.Add(new StudentDto()
            {

                StudentId = a.StudentId,
                FirstName = a.FirstName,
                LastName = a.LastName,





            }));





        }

        // GET: api/StudentsData/Findstudents
        [ResponseType(typeof(Students))]
        [HttpGet]
        public IHttpActionResult FindStudents(int id)
        {
            Students students = db.Student.Find(id);
            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // PUT: api/StudentsData/updateStudents
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateStudents(int id, Students students)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != students.StudentId)
            {
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StudentsData/AddAnimal
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