using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Models {
	public class Applicant {
		public int ID { get; set; }
		public string Name { get; set; }
		public string FamilyName { get; set; }
		public string CountryOfOrigin { get; set; }
		public string EMailAdress { get; set; }
		public int Age { get; set; }
		public bool Hired { get; set; }
	}
}
