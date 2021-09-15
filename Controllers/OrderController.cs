using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Faker;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SlothBookStore.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{orderno}")]
        public ActionResult Get(string orderno)
        {
            var o = new OrderRsp
            {
                OrderNo = orderno,
                OrderItem = Faker.Company.Name(),
                CustomerName = Faker.Name.FullName(),
                Qty = Faker.RandomNumber.Next(1, 10),
            };

            return Ok(o);
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }

    public class OrderReq
    { 
        public string CustomerName { get; set; }
        public string OrderItem { get; set; }
        public int Qty { get; set; }
    }

    public class OrderRsp
    { 
        public string OrderNo { get; set; }
        public string CustomerName { get; set; }
        public string OrderItem { get; set; }
        public int Qty { get; set; }
    }
}
