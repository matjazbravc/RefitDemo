using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

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
