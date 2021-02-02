using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicationProcess.December2020.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicantController : ControllerBase {
		// GET: api/<ApplicantController>
		[HttpGet]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<ApplicantController>/5
		[HttpGet("{id}")]
		public string Get(int id) {
			return "value";
		}

		// POST api/<ApplicantController>
		[HttpPost]
		public void Post([FromBody] string value) {
		}

		// PUT api/<ApplicantController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
		}

		// DELETE api/<ApplicantController>/5
		[HttpDelete("{id}")]
		public void Delete(int id) {
		}
	}
}
