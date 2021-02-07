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
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.December2020.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicantController : ControllerBase {
		private IApplicantManager _applicantManager;
		private readonly ILogger<ApplicantController>  _logger;

		public ApplicantController(IApplicantManager manager, ILogger<ApplicantController> logger) {
			_applicantManager = manager;
			_logger = logger;
		}

		/// <summary>
		/// Return the applicant for the provided ID number.
		/// </summary>
		/// <param name="id" example="123">The applicant's id</param>
		/// <response code="200">Returns the requested applicant</response>
		[HttpGet("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(typeof(Applicant), StatusCodes.Status200OK)]
		public async Task<ActionResult<Applicant>> GetAsync(int id) {
			try {
				var applicant = await _applicantManager.GetApplicantAsync(id);
				if (applicant != null) {
					_logger.LogInformation($"The applicant with ID: {id} has been found!");
					return applicant;
				} else
					_logger.LogInformation($"The applicant with ID: {id} has not been found!");
					return NotFound();
			} catch (Exception) {
				_logger.LogError($"An error occurred while trying to read the entry from the database. An applicant with ID: {id} has been requested!");
				return BadRequest();
			}
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
			//if (ModelState.IsValid)  has become unnecessary. that logic is now applied automatically, ie BadRequest with added errors is called  
			try {
				var createdApplicantId = _applicantManager.AddApplicant(applicant);
				_logger.LogInformation($"An applicant with ID: {createdApplicantId} has been created in the database!");
				return CreatedAtAction(nameof(GetAsync), createdApplicantId); // createdApplicant can not be null, so no null-checking 
			} catch (Exception) {
				_logger.LogError("An error occurred while trying to create a new entry. New applicant is not saved to the database.");
			}
			return BadRequest();
		}

		/// <summary>
		/// Update an existing applicant
		/// </summary>
		/// <response code="200">Applicant updated</response>
		[HttpPut("{id}")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult Put(int id, [FromBody, SwaggerRequestBody("The applicant entry", Required = true)] Applicant applicant) {
			try {
				var  updateSuccessful = _applicantManager.UpdateApplicant(applicant);
				if (updateSuccessful) {
					_logger.LogInformation($"The applicant with ID: {applicant.ID} updated successfully!");
					return Ok();
				} else {
					_logger.LogWarning($"The applicant with ID: {applicant.ID} has not been found in the database!");
					return NotFound();
				}
			} catch (Exception) {
				_logger.LogError($"An error occurred while trying to update the applicant with ID: {applicant.ID} Nothing has been changed!");
			
			}
			return BadRequest();
		}

		/// <summary>
		/// Delete an existing applicant
		/// </summary>
		/// <param name="id" example="123">The applicant's id</param>
		/// <response code="200">Applicant deleted</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult Delete(int id) {
			try {
				var success = _applicantManager.DeleteApplicant(id);
				if (success == 0) {
					_logger.LogWarning($"Attempt to delete a non-existing applicant! Applicant with ID: {id} has not been found in the database!");
					return NotFound();
				} else {
					_logger.LogInformation($"Applicant wit ID: {id} has been deleted from the database!");
					return Ok();
				}
			} catch (Exception) {
				_logger.LogError($"Unsuccessful attempt to delete an entry from the database! There was an error while trying to delete an applicant with ID: {id}");
				return BadRequest(); 
			}
		}
	}
}