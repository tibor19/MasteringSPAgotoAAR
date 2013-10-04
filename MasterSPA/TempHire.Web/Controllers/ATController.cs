using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TempHire.DomainModel;
using TempHire.Web.Models;

namespace TempHire.Web.Controllers
{
    public class ATController : ApiController
    {
        private readonly TempHireDBContext _context = new TempHireDBContext();

        // GET api/AT
        public IEnumerable<AddressType> GetAddressTypes()
        {
            return _context.AddressTypes.AsEnumerable();
        }

        // GET api/AT/5
        public AddressType GetAddressType(Guid id)
        {
            AddressType addresstype = _context.AddressTypes.Find(id);
            if (addresstype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return addresstype;
        }

        // PUT api/AT/5
        public HttpResponseMessage PutAddressType(Guid id, AddressType addresstype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != addresstype.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _context.Entry(addresstype).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/AT
        public HttpResponseMessage PostAddressType(AddressType addresstype)
        {
            if (ModelState.IsValid)
            {
                _context.AddressTypes.Add(addresstype);
                _context.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, addresstype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = addresstype.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AT/5
        public HttpResponseMessage DeleteAddressType(Guid id)
        {
            AddressType addresstype = _context.AddressTypes.Find(id);
            if (addresstype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _context.AddressTypes.Remove(addresstype);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, addresstype);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}