using FluentValidation;
using Hahn.ApplicationProcess.December2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain.Validations {
	public class ApplicantValidator : AbstractValidator<Applicant> {
		public ApplicantValidator() {
			RuleFor(a => a.Name).MinimumLength(5);
			RuleFor(a => a.FamilyName).MinimumLength(5);
			RuleFor(a => a.Address).MinimumLength(10);
			RuleFor(a => a.EMailAdress).EmailAddress();
			RuleFor(a => a.Age).InclusiveBetween(20, 60);
			RuleFor(a => a.CountryOfOrigin).MustBeValidCountry();
			RuleFor(a => a.Hired).NotNull();
			// todo .WithMessage("message");
		}
	}
}
