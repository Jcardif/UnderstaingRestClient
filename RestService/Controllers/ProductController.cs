using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestService.Models;
using RestService.Operations;

namespace RestService.Controllers
{
    public class ProductController : ApiController
    {
        // GET: api/Product
        public List<Product> Get()
        {
            var lstProduct = new ProductPersistence().GetAllProducts();
            if(lstProduct==null)
                throw new HttpResponseException(HttpStatusCode.NoContent);
            return lstProduct;
        }

        // GET: api/Product/5
        public Product Get(int id)
        { 
            var product=new ProductPersistence().GetProdut(id);
            if (product==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return product;
        }

        // POST: api/Product
        public HttpResponseMessage Post([FromBody]Product product)
        {
            var id=new ProductPersistence().CreateProduct(product);
            var response=new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location=new Uri(Request.RequestUri, string.Format($"Product/{id}"));
            return response;
        }

        // PUT: api/Product/5
        public HttpResponseMessage Put(int id, [FromBody]Product product)
        {
            bool isInRecord=new ProductPersistence().UpdateProduct(id, product);
            var response = isInRecord
                ? new HttpResponseMessage(HttpStatusCode.NoContent)
                : new HttpResponseMessage(HttpStatusCode.NotFound);
            return response;
        }

        // DELETE: api/Product/5
        public HttpResponseMessage Delete(int id)
        {
            bool isInRecord = new ProductPersistence().DeleteProduct(id);
            var response = isInRecord
                ? new HttpResponseMessage(HttpStatusCode.NoContent)
                : new HttpResponseMessage(HttpStatusCode.NotFound);
            return response;
        }
    }
}
