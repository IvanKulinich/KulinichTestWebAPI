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
    public class OrderProductsController : ApiController
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: api/OrderProduct
        [Route("api/OrderProduct")]
        public HttpResponseMessage GetOrderProducts()
        {
            var orderProducts = db.OrderProducts;
            if (orderProducts != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, orderProducts);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
            }            
        }

        // POST: api/OrderProduct
        [Route("api/OrderProduct")]
        public HttpResponseMessage PostOrderProduct([FromBody]OrderProduct orderProduct)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        
            if (orderProduct != null)
            {
                var productInDB = db.Products.Find(orderProduct.ProductId);

                var orderInDB = db.Orders.Find(orderProduct.OrderId);
                if (productInDB != null && orderInDB != null)
                {
                    var newOrderProduct = new OrderProduct()
                    {
                        OrderId = orderInDB.Id,
                        ProductId = productInDB.Id
                    };
                    db.OrderProducts.Add(orderProduct);
                    db.SaveChanges();

                    var alreadyInDB = db.OrderProducts.Where(m => m.OrderId == orderInDB.Id)
                        .Where(n => n.ProductId == productInDB.Id)
                        .FirstOrDefault();
                    newOrderProduct.Id = alreadyInDB.Id;
                    return Request.CreateResponse(HttpStatusCode.OK, newOrderProduct);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad data");
                }               
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad data");
            }
        }

        // DELETE: api/OrderProduct
        [Route("api/OrderProduct")]
        public HttpResponseMessage DeleteAllOrderProducts()
        {
            try
            {
                var orderProducts = db.OrderProducts;
                if (orderProducts != null && orderProducts.Any())
                {
                    db.OrderProducts.RemoveRange(orderProducts);
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

        private bool OrderProductExists(Guid id)
        {
            return db.OrderProducts.Count(e => e.Id == id) > 0;
        }
    }
}