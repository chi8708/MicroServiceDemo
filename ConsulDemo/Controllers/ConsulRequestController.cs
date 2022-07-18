using Consul;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulDemo.Controllers
{
 
    [ApiController]
    public class ConsulRequestController : ControllerBase
    {
        [Route("api/ConsulRequest")]
        public string  Get() 
        {
            ConsulClient client = new ConsulClient(c => {
                c.Address = new Uri("http://localhost:8500/");
                c.Datacenter = "dc1";
            });

            string groupName = "service";
            var response = client.Agent.Services().Result.Response
                .Where(x => x.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase)).ToList();

            var url = $"https://{response.First().Value.Address}:{response.First().Value.Port}";
            return url;
        }
    } 
}
