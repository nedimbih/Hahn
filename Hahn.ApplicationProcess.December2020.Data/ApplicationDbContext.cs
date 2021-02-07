using Hahn.ApplicationProcess.December2020.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data {
	public class ApplicationDbContext : DbContext {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) {
		}

		public DbSet<Applicant> Applicants { get; set; }
	}
}
