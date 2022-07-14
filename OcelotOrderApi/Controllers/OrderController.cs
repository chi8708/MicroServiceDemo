using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotOrderApi.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/Product
        [Route("api/orders")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "订单1", "订单2" };
        }

        // GET: api/Product
        [Route("api/orders/{id}")]
        [HttpGet]
        public dynamic Get(int id)
        {
            //Task.Delay(3000).Wait();
            return new { info="订单1" };
        }


        [Route("api/shoppingCarts")]
        [HttpGet]
        public IEnumerable<string> ShoppingCarts()
        {
            Console.WriteLine($"开始打印header信息");
            List<string> headers = new List<string>();
            foreach (var item in this.Request.Headers)
            {
                headers.Add($"{item.Key}:{item.Value}");
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            Console.WriteLine($"打印header信息完成");

            return headers;
        }

    }
}
