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
    public class usrsController : ApiController
    {
        private TMOEntities db = new TMOEntities();

        // GET: api/usrs
        public IQueryable<usr> Getusrs()
        {
            return db.usrs;
        }

        // GET: api/usrs/5
        [ResponseType(typeof(usr))]
        public IHttpActionResult Getusr(int id)
        {
            usr usr = db.usrs.Find(id);
            if (usr == null)
            {
                return NotFound();
            }

            return Ok(usr);
        }

        // PUT: api/usrs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putusr(int id, usr usr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usr.unique_id)
            {
                return BadRequest();
            }

            db.Entry(usr).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usrExists(id))
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

        // POST: api/usrs
        [ResponseType(typeof(usr))]
        public IHttpActionResult Postusr(usr usr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.usrs.Add(usr);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usr.unique_id }, usr);
        }

        // DELETE: api/usrs/5
        [ResponseType(typeof(usr))]
        public IHttpActionResult Deleteusr(int id)
        {
            usr usr = db.usrs.Find(id);
            if (usr == null)
            {
                return NotFound();
            }

            db.usrs.Remove(usr);
            db.SaveChanges();

            return Ok(usr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usrExists(int id)
        {
            return db.usrs.Count(e => e.unique_id == id) > 0;
        }
    }
}