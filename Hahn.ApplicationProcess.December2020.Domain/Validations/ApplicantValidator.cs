using FluentValidation;
using Hahn.ApplicationProcess.December2020.Models;

namespace Hahn.ApplicationProcess.December2020.Domain.Validations {
	public class ApplicantValidator : AbstractValidator<Applicant> {
		public ApplicantValidator() {
			RuleFor(a => a.Name).MinimumLength(5).WithMessage("Name must have at least 5 characters!");
			RuleFor(a => a.FamilyName).MinimumLength(5).WithMessage("Family name must have at least 5 characters!");
			RuleFor(a => a.Address).MinimumLength(10).WithMessage("Address must have at least 10 characters!");
			RuleFor(a => a.EMailAdress).EmailAddress().WithMessage("Email must be a valid email address!");
			RuleFor(a => a.Age).InclusiveBetween(20, 60).WithMessage("Age must be between 20 and 60!");
			RuleFor(a => a.CountryOfOrigin).MustBeValidCountry().WithMessage("Please enter a valid country");
			RuleFor(a => a.Hired).NotNull();
		}
	}
}
