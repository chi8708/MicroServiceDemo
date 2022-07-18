using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotOrderApi.Controllers
{
    
    [ApiController]
    public class Health : ControllerBase
    {
        [Route("api/[controller]")]
        public string Get() 
        {
            return "健康检查ok...";
        
        }
    }
}
