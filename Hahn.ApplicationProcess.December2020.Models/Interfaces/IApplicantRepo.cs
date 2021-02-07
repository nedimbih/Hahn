using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Models.Interfaces {
	public interface IApplicantRepo {
		Task<Applicant> GetApplicantAsync(int id);
		int AddApplicant(Applicant applicant);
		Applicant UpdateApplicant(int id, Applicant applicant);
		int DeleteApplicant(int id);
	}
}
