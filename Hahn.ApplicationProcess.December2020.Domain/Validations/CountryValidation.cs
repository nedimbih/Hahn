using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain.Validations {
	public static class CountryValidation {

		public static IRuleBuilderOptions<T, TElement> MustBeValidCountry<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder) {

			return ruleBuilder.SetValidator(new CountryValidator<string>());
		}
	}


	public class CountryValidator<T> : PropertyValidator {

		protected override bool IsValid(PropertyValidatorContext context) {
			var country = context.PropertyValue as string;

			using HttpClient http = new();
			var request = http.GetAsync($"https://restcountries.eu/rest/v2/name/{country}?fullText=true");
			
			// Do not use HttpStatusCode.NotFound as the condition, because there might be other errors with different status code
			if (request.Result.StatusCode != HttpStatusCode.OK) {
				context.MessageFormatter.AppendArgument("Country", country);
				return false;
			}
			return true;
		}

		protected override string GetDefaultMessageTemplate()
		=> "{Country} is not a valid country name!";
	}
}
