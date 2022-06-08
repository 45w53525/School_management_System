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
    public class SubjectsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SubjectsData/ListSubjects
        [HttpGet]
        public IEnumerable<SubjectsDto> ListSubjects()
        {
            List<Subjects> Subjects = db.Subject.ToList();
            List<SubjectsDto> SubjectDtos = new List<SubjectsDto>();

            Subjects.ForEach(b => SubjectDtos.Add(new SubjectsDto()
            {
                SubjectId = b.SubjectId,

                SubjectName = b.SubjectName,

                SubjectFees = b.SubjectFees



            }));




            return SubjectDtos;
        }

        // GET: api/SubjectsData/5
        [ResponseType(typeof(Subjects))]
        [HttpGet]
        public IHttpActionResult FindSubjects(int id)
        {
            Subjects subjects = db.Subject.Find(id);
            SubjectsDto subjectsDto = new SubjectsDto()
            {
                SubjectId = subjects.SubjectId,

                SubjectName = subjects.SubjectName,

                SubjectFees = subjects.SubjectFees

            };

            if (subjects == null)
            {
                return NotFound();
            }

            return Ok(subjectsDto);
        }

        // Post: api/SubjectsData/updateSubject/3
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSubjects(int id, Subjects subjects)
        {
            Debug.WriteLine("I have reached the update subject method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != subjects.SubjectId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + subjects.SubjectId);
                Debug.WriteLine("POST parameter" + subjects.SubjectName);
                Debug.WriteLine("POST parameter " + subjects.SubjectFees);
                return BadRequest();
            }

            db.Entry(subjects).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectsExists(id))
                {
                    Debug.WriteLine("Subject not found");
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

        // POST: api/SubjectsData/AddSubject
        [ResponseType(typeof(Subjects))]
        [HttpPost]
        public IHttpActionResult AddSubjects(Subjects subjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subject.Add(subjects);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subjects.SubjectId }, subjects);
        }

        // DELETE: api/SubjectsData/5
        [ResponseType(typeof(Subjects))]
        [HttpPost]
        public IHttpActionResult DeleteSubjects(int id)
        {
            Subjects subjects = db.Subject.Find(id);
            if (subjects == null)
            {
                return NotFound();
            }

            db.Subject.Remove(subjects);
            db.SaveChanges();

            return Ok(subjects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectsExists(int id)
        {
            return db.Subject.Count(e => e.SubjectId == id) > 0;
        }
    }
}