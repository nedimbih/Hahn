using Hahn.ApplicationProcess.December2020.Models;
using Hahn.ApplicationProcess.December2020.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain {

	// This class does not have try-catch blocks because controller catches exception 
	// Actions might be without null checking because corresponding action in _repo doesn't ever return null
	public class ApplicantManager : IApplicantManager {
		private IApplicantRepo _repo;

		public ApplicantManager(IApplicantRepo repo) {
			_repo = repo;
		}

		public int AddApplicant(Applicant applicant) {
			applicant.ID = 0; // sets to zero so db can generate an id number. 
			return _repo.AddApplicant(applicant);
		} 

		public int DeleteApplicant(int id) => 
			_repo.DeleteApplicant(id);
		public async Task<Applicant> GetApplicantAsync(int id) =>
			await _repo.GetApplicantAsync(id);
		public bool UpdateApplicant(int id, Applicant applicant) {
			var updatedApplicant = _repo.UpdateApplicant(id, applicant);
			return updatedApplicant != null;
		}
	}
}
