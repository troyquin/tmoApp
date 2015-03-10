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
    public class ccyrsController : ApiController
    {
        private TMOEntities db = new TMOEntities();

        // GET: api/ccyrs
        public IQueryable<ccyr> Getccyrs()
        {
            return db.ccyrs;
        }

        // GET: api/ccyrs/5
        [ResponseType(typeof(ccyr))]
        public IHttpActionResult Getccyr(int id)
        {
            ccyr ccyr = db.ccyrs.Find(id);
            if (ccyr == null)
            {
                return NotFound();
            }

            return Ok(ccyr);
        }

        // PUT: api/ccyrs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putccyr(int id, ccyr ccyr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ccyr.unique_id)
            {
                return BadRequest();
            }

            db.Entry(ccyr).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ccyrExists(id))
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

        // POST: api/ccyrs
        [ResponseType(typeof(ccyr))]
        public IHttpActionResult Postccyr(ccyr ccyr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ccyrs.Add(ccyr);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ccyr.unique_id }, ccyr);
        }

        // DELETE: api/ccyrs/5
        [ResponseType(typeof(ccyr))]
        public IHttpActionResult Deleteccyr(int id)
        {
            ccyr ccyr = db.ccyrs.Find(id);
            if (ccyr == null)
            {
                return NotFound();
            }

            db.ccyrs.Remove(ccyr);
            db.SaveChanges();

            return Ok(ccyr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ccyrExists(int id)
        {
            return db.ccyrs.Count(e => e.unique_id == id) > 0;
        }
    }
}