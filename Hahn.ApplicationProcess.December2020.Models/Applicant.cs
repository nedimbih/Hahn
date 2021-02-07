using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hahn.ApplicationProcess.December2020.Models {
	public class Applicant {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		// summaries (for swagger) are intentionally omitted since all property names are easy to understand
		// sets example value for swagger
		/// <example>Veronika</example>
		[MinLength(5, ErrorMessage = "Name can not be shorter than 5 characters!")]
		[MaxLength(100, ErrorMessage = "Name can not be longer than 100 characters!")]
		public string Name { get; set; }

		/// <example>Jackson</example>
		[MinLength(5, ErrorMessage = "Family name can not be shorter than 5 characters!")]
		[MaxLength(100, ErrorMessage = "Family name can not be longer than 5 characters!")]
		public string FamilyName { get; set; }

		/// <example>Green street 23, London</example>
		[MinLength(10, ErrorMessage = "Address can not be shorter than 10 characters!")]
		[MaxLength(200, ErrorMessage = "Address can not be longer than 200 characters!")]
		public string Address { get; set; }

		/// <example>Fiji</example>
		[MinLength(2, ErrorMessage = "Country can not be shorter than 2 characters!")]
		[MaxLength(100, ErrorMessage = "Country can not be longer than 100 characters!")]
		public string CountryOfOrigin { get; set; }

		/// <example>someone@email.me</example>
		[MinLength(5, ErrorMessage = "Email can not be shorter than 5 characters!")]
		[MaxLength(320, ErrorMessage = "Email can not be longer than 320 characters!")]
		public string EMailAdress { get; set; }

		/// <example>43</example>
		[Range(20, 60, ErrorMessage = "Age must be between 20 and 60!")]
		public int Age { get; set; }

		/// <example>true</example>
		public bool Hired { get; set; }
	}
}
