using Hahn.ApplicationProcess.December2020.Models;
using Hahn.ApplicationProcess.December2020.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Data {
	public class ApplicantRepo : IApplicantRepo {
		private ApplicationDbContext _context;

		public ApplicantRepo(ApplicationDbContext applicationDb) {
			_context = applicationDb;
		}

		public int AddApplicant(Applicant applicant) {
			try {
				var created = _context.Add<Applicant>(applicant);

				if (_context.SaveChanges() > 0) {
					return created.Entity.ID;
				} else
					throw new Exception("Db Error: Entry is not saved!");
			} catch (Exception) { throw; }
		}
		public int DeleteApplicant(int id) {
			try {
				var applicant = _context.Applicants.FirstOrDefault(p => p.ID == id);
				if (applicant == null)
					return 0;
				_context.Applicants.Remove(applicant);
				return _context.SaveChanges();
			} catch (Exception) { throw; }
		}
		public async Task<Applicant> GetApplicantAsync(int id) {
			try {
				return await _context.Applicants
					.FirstOrDefaultAsync<Applicant>(a => a.ID == id);
			} catch (Exception) { throw; }
		}
		public Applicant UpdateApplicant(int id, Applicant applicant) {
			try {
				var entryInDb = _context.Applicants
							.FirstOrDefault(a => a.ID == id);
				if (entryInDb == null)
					return null;

				PopulateProperties(entryInDb, applicant);

				var updated = _context.Update(entryInDb);
				if (_context.SaveChanges() > 0) {
					return updated.Entity;
				} else
					throw new Exception("Db Error: Entry is not saved!");
			} catch (Exception) { throw; }
		}

		private void PopulateProperties(Applicant entryInDb, Applicant incomingValues) {
			if (incomingValues.Name is not null)
				entryInDb.Name = incomingValues.Name;
			if (incomingValues.FamilyName is not null)
				entryInDb.FamilyName = incomingValues.FamilyName;
			if (incomingValues.Address is not null)
				entryInDb.Address = incomingValues.Address;
			if (incomingValues.CountryOfOrigin is not null)
				entryInDb.CountryOfOrigin = incomingValues.CountryOfOrigin;
			if (incomingValues.EmailAddress is not null)
				entryInDb.EmailAddress = incomingValues.EmailAddress;
			if (incomingValues.Age != 0)
				entryInDb.Age = incomingValues.Age;
			if (incomingValues.Hired)
				entryInDb.Hired = incomingValues.Hired;
		}
	}
}
