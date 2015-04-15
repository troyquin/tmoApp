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
using System.Web.Http.ValueProviders;
using tmoWebAPI3.Infrastructure;
using tmoWebAPI3.Models;

namespace tmoWebAPI3.Controllers
{
		[RoutePrefix("api")]
    public class usrsController : ApiController
    {
        private TMOEntities db = new TMOEntities();
				private const string LinkHeaderTemplate = "<{0}>; rel=\"{1}\"";

				// GET: api/usrs
				[HttpGet]
				[Route("usrs/headers")]
				public HttpResponseMessage GetViaHeaders([ValueProvider(typeof(XHeaderValueProviderFactory))] int pageNo = 1, [ValueProvider(typeof(XHeaderValueProviderFactory))] int pageSize = 20)
				{
					// Determine the number of records to skip
					int skip = (pageNo - 1) * pageSize;

					// Get total number of records
					int total = db.usrs.Count();

					// Select the users based on paging parameters
					var users = db.usrs
						.OrderBy(u => u.usrs_login)
						.Skip(skip)
						.Take(pageSize)
						.ToList();

					// Determine page count
					int pageCount = total > 0
							? (int)Math.Ceiling(total / (double)pageSize)
							: 0;

					// Create the response
					var response = Request.CreateResponse(HttpStatusCode.OK, users);

					// Set headers for paging
					response.Headers.Add("X-Paging-PageNo", pageNo.ToString());
					response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
					response.Headers.Add("X-Paging-PageCount", pageCount.ToString());
					response.Headers.Add("X-Paging-TotalRecordCount", total.ToString());

					// Return the response
					return response;
				}

				[HttpGet]
				[Route("usrs/pagelinks", Name = "GetPageLinks")]
				public IHttpActionResult GetPageLinks(int pageNo = 1, int pageSize = 20)
				{
					// Determine the number of records to skip
					int skip = (pageNo - 1) * pageSize;

					// Get total number of records
					int total = db.usrs.Count();

					// Select the customers based on paging parameters
					var users = db.usrs
							.OrderBy(u => u.usrs_login)
							.Skip(skip)
							.Take(pageSize)
							.ToList();

					// Get the page links
					var linkBuilder = new PageLinkBuilder(Url, "GetPageLinks", null, pageNo, pageSize, total);

					// Return the list of customers
					return Ok(new
					{
						Data = users,
						Paging = new
						{
							First = linkBuilder.FirstPage,
							Previous = linkBuilder.PreviousPage,
							Next = linkBuilder.NextPage,
							Last = linkBuilder.LastPage
						}
					});
				}

				[HttpGet]
				[Route("usrs/pagelinkheaders", Name = "GetPageLinkHeaders")]
				public HttpResponseMessage GetPageLinkHeaders(int pageNo = 1,  int pageSize = 20)
				{
					// Determine the number of records to skip
					int skip = (pageNo - 1) * pageSize;

					// Get total number of records
					int total = db.usrs.Count();

					// Select the customers based on paging parameters
					var users = db.usrs
							.OrderBy(u => u.usrs_login)
							.Skip(skip)
							.Take(pageSize)
							.ToList();

					// Get the page links
					var linkBuilder = new PageLinkBuilder(Url, "GetPageLinkHeaders", null, pageNo, pageSize, total);

					// Create the response
					var response = Request.CreateResponse(HttpStatusCode.OK, users);

					// Build up the link header
					List<string> links = new List<string>();
					if (linkBuilder.FirstPage != null)
						links.Add(string.Format(LinkHeaderTemplate, linkBuilder.FirstPage, "first"));
					if (linkBuilder.PreviousPage != null)
						links.Add(string.Format(LinkHeaderTemplate, linkBuilder.PreviousPage, "previous"));
					if (linkBuilder.NextPage != null)
						links.Add(string.Format(LinkHeaderTemplate, linkBuilder.NextPage, "next"));
					if (linkBuilder.LastPage != null)
						links.Add(string.Format(LinkHeaderTemplate, linkBuilder.LastPage, "last"));

					// Determine page count
					int pageCount = total > 0
							? (int)Math.Ceiling(total / (double)pageSize)
							: 0;				

					// Set headers for paging
					response.Headers.Add("X-Paging-PageNo", pageNo.ToString());
					response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
					response.Headers.Add("X-Paging-PageCount", pageCount.ToString());
					response.Headers.Add("X-Paging-TotalRecordCount", total.ToString());

					// Set the page link header
					response.Headers.Add("Link", string.Join(", ", links));

					// Return the response
					return response;
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