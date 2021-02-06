using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Models.Interfaces;
using Hahn.ApplicationProcess.December2020.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Serilog;

namespace Hahn.ApplicationProcess.December2020.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicantController : ControllerBase {
		private IApplicantManager _applicantManager;
		private readonly ILogger _loger;

		public ApplicantController(IApplicantManager manager, ILogger loger) {
			_applicantManager = manager;
			_loger = loger;
		}

		/// <summary>
		/// Return the applicant for the provided ID number.
		/// </summary>
		/// <param name="id" example="123">The applicant's id</param>
		/// <response code="200">Returns the requested applicant</response>
		[HttpGet("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(typeof(Applicant), StatusCodes.Status200OK)]
		public IActionResult Get(int id) {
			return NotFound();
		}

		/// <summary>
		/// Create a new applicant entry
		/// </summary>
		/// <response code="201">Applicant stored in db</response>
		/// <remarks>
		/// If the entry is stored successfully, url of the stored entry will be returned
		/// </remarks>
		[HttpPost]
		[Consumes("application/json")]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		public ActionResult Post([FromBody, SwaggerRequestBody("The applicant entry", Required = true)] Applicant applicant) {
			return new object() as ActionResult;
		}

		/// <summary>
		/// Update an existing applicant
		/// </summary>
		/// <response code="200">Applicant updated</response>
		[HttpPut("{id}")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public void Put(int id, [FromBody, SwaggerRequestBody("The applicant entry", Required = true)] Applicant applicant) {
		}

		/// <summary>
		/// Delete an existing applicant
		/// </summary>
		/// <param name="id" example="123">The applicant's id</param>
		/// <response code="200">Applicant deleted</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public void Delete(int id) {
		}
	}
}
