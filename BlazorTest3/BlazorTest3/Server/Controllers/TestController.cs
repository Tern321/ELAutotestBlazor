using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTest3.Shared;
using Microsoft.AspNetCore.Mvc;
//using BlazorTest3.Server.Model1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorTest3.Server.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]
    public class TestController : Controller
    {
        

        [HttpGet("CreateTestCase/{testName}")]
        public void CreateTestCase(string testName)
        {
            Console.WriteLine("CreateTestCase backend");
            var manager = Model1.init();

            //var testRun = new ELTestRun();
            //testRun.Id = "test run 1";
            //testRun.DeviceId = "123";
            //return new ELTestRun[] { testRun };
        }


        [HttpGet("TestRuns/{testName}")]
        public IEnumerable<ELTestRun> testRuns(string testName)
        {
            var testRun = new ELTestRun();
            testRun.Id = "test run 1";
            testRun.DeviceId = "123";
            return new ELTestRun[] { testRun};
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

