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
    public class ServicesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ServicesData
        public IQueryable<Services> GetService()
        {
            return db.Service;
        }

        // GET: api/ServicesData/5
        [ResponseType(typeof(Services))]
        public IHttpActionResult GetServices(int id)
        {
            Services services = db.Service.Find(id);
            if (services == null)
            {
                return NotFound();
            }

            return Ok(services);
        }

        // PUT: api/ServicesData/5/ 
        // PUT: api/ServicesData/5/

        [ResponseType(typeof(void))]
        public IHttpActionResult PutServices(int id, Services services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != services.ServiceId)
            {
                return BadRequest();
            }

            db.Entry(services).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesExists(id))
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

        // POST: api/ServicesData
        [ResponseType(typeof(Services))]
        public IHttpActionResult PostServices(Services services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Service.Add(services);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = services.ServiceId }, services);
        }

        // DELETE: api/ServicesData/5
        [ResponseType(typeof(Services))]
        public IHttpActionResult DeleteServices(int id)
        {
            Services services = db.Service.Find(id);
            if (services == null)
            {
                return NotFound();
            }

            db.Service.Remove(services);
            db.SaveChanges();

            return Ok(services);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServicesExists(int id)
        {
            return db.Service.Count(e => e.ServiceId == id) > 0;
        }
    }
}