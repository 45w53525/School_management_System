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
    public class TeachersDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TeachersData
        [HttpGet]
        public IEnumerable<TeacherDto> ListTeacher()
        {
            List<Teacher> Teacher = db.Teacher.ToList();
            List<TeacherDto> TeacherDtos = new List<TeacherDto>();

            Teacher.ForEach(a => TeacherDtos.Add(new TeacherDto()
            {
                        TeacherId = a.TeacherId,
                TeacherFirstName = a.TeacherFirstName,
                TeacherLastName = a.TeacherLastName,
                TeachingSubject1 = a.TeachingSubject1
            })) ;

            return TeacherDtos;
        }

        // GET: api/TeachersData/FindTeacher/2
        [ResponseType(typeof(Teacher))]
        [HttpGet]
        public IHttpActionResult GetTeacher(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            TeacherDto TeacherDto = new TeacherDto()
            {
                TeacherId = teacher.TeacherId,
                TeacherFirstName = teacher.TeacherFirstName,
                TeacherLastName = teacher.TeacherLastName,
                TeachingSubject1=teacher.TeachingSubject1

            };
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(TeacherDto);
        }

        // PostT: api/TeachersData/updateTeacher/2
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PutTeacher(int id, Teacher teacher)
        {
            Debug.WriteLine("I have reached the update animal method!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.TeacherId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + teacher.TeacherId);
                Debug.WriteLine("POST parameter" + teacher.TeacherFirstName);
                Debug.WriteLine("POST parameter " + teacher.TeacherLastName);
                Debug.WriteLine("POST parameter " + teacher.TeachingSubject1);
                return BadRequest();
            }

            db.Entry(teacher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    Debug.WriteLine("Animal not found");
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

        // POST: api/TeachersData/AddTeacher/
        [ResponseType(typeof(Teacher))]
        [HttpPost]
        public IHttpActionResult PostTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teacher.Add(teacher);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teacher.TeacherId }, teacher);
        }

        // Post: api/TeachersData/DeleteTeacher/2
        [ResponseType(typeof(Teacher))]
        [HttpPost]
        public IHttpActionResult DeleteTeacher(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            db.Teacher.Remove(teacher);
            db.SaveChanges();

            return Ok(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherExists(int id)
        {
            return db.Teacher.Count(e => e.TeacherId == id) > 0;
        }
    }
}