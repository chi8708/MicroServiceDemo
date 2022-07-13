using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OcelotProductApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public IEnumerable<string> Get()
        {
            return new string[] { "产品1", "产品2" };
        }
    }
}
