using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RefitDemo.Entities
{
	[Serializable]
	[JsonObject(IsReference = false)]
	public class EmployeeAddress
	{
		[Key, ForeignKey(nameof(Employee))]
		public int EmployeeId { get; set; }
		
        // Navigation property
		public Employee Employee { get; set; }

		public string Address { get; set; }
	}
}
