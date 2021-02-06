using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Models {
	public class Applicant {
		public int ID { get; set; }

		// summaries (for swagger) are intentionally omitted since all property names are easy to understand
		// sets example value for swagger
		/// <example>Veronika</example>
		public string Name { get; set; }

		/// <example>Jackson</example>
		public string FamilyName { get; set; }

		/// <example>Green street 23, London</example>
		public string Address { get; set; }

		/// <example>Fiji</example>
		public string CountryOfOrigin { get; set; }

		/// <example>someone@email.me</example>
		public string EMailAdress { get; set; }

		/// <example>43</example>
		public int Age { get; set; }

		/// <example>true</example>
		public bool Hired { get; set; }
	}
}
