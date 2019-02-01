using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RefitDemo.Entities
{
	// https://www.tektutorialshub.com/entity-framework-core-data-seeding/
	[Serializable]
	[JsonObject(IsReference = false)]
	public class Department
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int DepartmentId { get; set; }

		public string Name { get; set; }
		
        // Navigation property
		public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public override string ToString() => $"{DepartmentId}, {Name}";
	}
}
