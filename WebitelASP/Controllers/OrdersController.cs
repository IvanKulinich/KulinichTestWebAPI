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
    public class OrdersController : ApiController
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: api/Order
        [Route("api/Order")]
        public HttpResponseMessage GetOrders()
        {
            var orders = db.Orders;
            if (orders != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }

        // POST: api/Order
        [Route("api/Order")]
        public HttpResponseMessage PostOrder([FromBody]Order order)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (order != null)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, order);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad data");
            }          
        }

        // DELETE: api/Order
        [Route("api/Order")]
        public HttpResponseMessage DeleteAllOrders()
        {
            try
            {
                var orders = db.Orders;
                if (orders != null && orders.Any())
                {
                    db.Orders.RemoveRange(orders);
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

        private bool OrderExists(Guid id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}