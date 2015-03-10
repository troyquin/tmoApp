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
using tmoWebAPI3.Models;

namespace tmoWebAPI3.Controllers
{
    public class cciesController : ApiController
    {
        private TMOEntities db = new TMOEntities();

        // GET: api/ccies
        public IQueryable<ccy> Getccies()
        {
            return db.ccies;
        }

        // GET: api/ccies/5
        [ResponseType(typeof(ccy))]
        public IHttpActionResult Getccy(int id)
        {
            ccy ccy = db.ccies.Find(id);
            if (ccy == null)
            {
                return NotFound();
            }

            return Ok(ccy);
        }

        // PUT: api/ccies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putccy(int id, ccy ccy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ccy.unique_id)
            {
                return BadRequest();
            }

            db.Entry(ccy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ccyExists(id))
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

        // POST: api/ccies
        [ResponseType(typeof(ccy))]
        public IHttpActionResult Postccy(ccy ccy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ccies.Add(ccy);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ccy.unique_id }, ccy);
        }

        // DELETE: api/ccies/5
        [ResponseType(typeof(ccy))]
        public IHttpActionResult Deleteccy(int id)
        {
            ccy ccy = db.ccies.Find(id);
            if (ccy == null)
            {
                return NotFound();
            }

            db.ccies.Remove(ccy);
            db.SaveChanges();

            return Ok(ccy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ccyExists(int id)
        {
            return db.ccies.Count(e => e.unique_id == id) > 0;
        }
    }
}