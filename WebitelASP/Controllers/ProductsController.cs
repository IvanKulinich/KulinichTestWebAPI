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
using WebitelASP.Models;

namespace WebitelASP.Controllers
{
    public class ProductsController : ApiController
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: api/Product
        [Route("api/Product")]
        public HttpResponseMessage GetProducts()
        {
            var products = db.Products;
            if (products != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, products);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }

        // POST: api/Product
        [Route("api/Product")]
        public HttpResponseMessage PostProduct([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (product != null)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, product);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad data");
            }
        }

        // DELETE: api/Product
        [Route("api/Product")]
        public HttpResponseMessage DeleteAllProducts()
        {
            try
            {
                var products = db.Products;
                if (products != null && products.Any())
                {
                    db.Products.RemoveRange(products);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "All records were deleted succesfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Table is already empty");
                }              
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(Guid id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}